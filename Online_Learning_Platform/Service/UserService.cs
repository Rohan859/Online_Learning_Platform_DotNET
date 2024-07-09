using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Online_Learning_Platform.AllDbContext;
using Online_Learning_Platform.DTOs;
using Online_Learning_Platform.Interfaces;
using Online_Learning_Platform.Model;

namespace Online_Learning_Platform.Service
{
    public class UserService : IUserService
    {
        private readonly AllTheDbContext _theDbContext;
        private readonly IMapper _mapper;

        public UserService(AllTheDbContext allTheDbContext, IMapper mapper)
        {
            _theDbContext = allTheDbContext;
            _mapper = mapper;
        }

        public string Register(UserRegistrationRequestDTO userRegistrationRequestDTO)
        {

            var user = _mapper.Map<User>(userRegistrationRequestDTO);
            user.UserId = Guid.NewGuid();

            _theDbContext.Users.Add(user);
            _theDbContext.SaveChanges();

            return $"User is registered in the database with id {user.UserId}";
        }


        public string DeleteUserById(Guid id)
        {
            User? user = _theDbContext.Users.Find(id);

            if (user == null)
            {
                return"User is not found";
            }


            _theDbContext.Users.Remove(user);
            _theDbContext.SaveChanges();

            return "Successfully deleted the user with id " + id;
        }


        public string UpdateUserProfile(UserProfileUpdateRequestDTO userProfileUpdateRequestDTO)
        {
            var user= _theDbContext.Users.Find(userProfileUpdateRequestDTO.UserId);
            if (user == null)
            {
                return "Not Found";
            }

            // var name = userProfileUpdateRequestDTO.UserName;
            //var password = userProfileUpdateRequestDTO.Password;
            // var mobile= userProfileUpdateRequestDTO.MobileNo;
            // var email= userProfileUpdateRequestDTO.Email;

            // if(name!=null)
            // {
            //     user.UserName= name;
            // }

            // if(password!=null)
            // {
            //     user.Password= password;
            // }

            // if(mobile!=null)
            // {
            //     user.MobileNo= mobile;
            // }

            // if (email!=null)
            // {
            //     user.Email= email;
            // }

            _mapper.Map(userProfileUpdateRequestDTO, user);

            //_theDbContext.Users.Update(user);
            _theDbContext.SaveChanges();

            return "User details got successfully updated";
        }


        public List<Course> GetCourseListForUserById(Guid userId)
        {
            var user = _theDbContext.Users
                .Include(x => x.Enrollments)
                .ThenInclude(x => x.Course)
                .FirstOrDefault(x => x.UserId == userId);

            List<Course> courseList = user?.Enrollments
                .Select(x => x.Course)
                .Distinct()
                .ToList()!;
                
            return courseList;
            

            //List<Course> courseList = _theDbContext.StudentCourses
            //        .Include(sc => sc.Course) // Include the Course entity in the query
            //        .Where(sc => sc.UserId == userId) // Filter by userId
            //        .Select(sc => sc.Course) // Select the Course entity from StudentCourses
            //        .ToList()!; // Materialize the result into a List<Course>

            //return courseList;
        }


        public int CountEnrollCoursesByUserId(Guid userId)
        {
            return GetCourseListForUserById(userId).Count();

            //List<Course> courseList = _theDbContext.StudentCourses
            //        .Include(sc => sc.Course) // Include the Course entity in the query
            //        .Where(sc => sc.UserId == userId) // Filter by userId
            //        .Select(sc => sc.Course) // Select the Course entity from StudentCourses
            //        .ToList()!; // Materialize the result into a List<Course>

            //return courseList.Count;
        }


        public int GetNoOfReviewsByUserId(Guid userId) 
        {
            var user = _theDbContext.Users
                .Include(u => u.Reviews)
                .FirstOrDefault (u => u.UserId == userId);


            if(user == null)
            {
                throw new Exception("User is not exist");
            }
            
            return user.Reviews.Count;
        }
    }
}
