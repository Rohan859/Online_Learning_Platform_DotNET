using Microsoft.EntityFrameworkCore;
using Online_Learning_Platform.AllDbContext;
using Online_Learning_Platform.DTOs;
using Online_Learning_Platform.Model;

namespace Online_Learning_Platform.Service
{
    public class UserService
    {
        private readonly AllTheDbContext _theDbContext;

        public UserService(AllTheDbContext allTheDbContext)
        {
            _theDbContext = allTheDbContext;
        }

        public string Register(UserRegistrationRequestDTO userRegistrationRequestDTO)
        {
            User user = new User
            {
                UserId = Guid.NewGuid(),
                UserName = userRegistrationRequestDTO.UserName,
                Password = userRegistrationRequestDTO.Password,
                Email = userRegistrationRequestDTO.Email,
                MobileNo = userRegistrationRequestDTO.MobileNo
            };


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

            var name = userProfileUpdateRequestDTO.UserName;
           var password = userProfileUpdateRequestDTO.Password;
            var mobile= userProfileUpdateRequestDTO.MobileNo;
            var email= userProfileUpdateRequestDTO.Email;

            if(name!=null)
            {
                user.UserName= name;
            }

            if(password!=null)
            {
                user.Password= password;
            }

            if(mobile!=null)
            {
                user.MobileNo= mobile;
            }

            if (email!=null)
            {
                user.Email= email;
            }

            _theDbContext.Users.Update(user);
            _theDbContext.SaveChanges();

            return "User details got successfully updated";
        }


        public List<Course> GetCourseListForUserById(Guid userId)
        {
            //var user = _theDbContext.Users.Find(userId);
            //if(user == null)
            // {
            //     return null;
            // }

            // var courseList = user.Courses;
            // return courseList;

            var user = _theDbContext.Users
            .Include(u => u.Courses) // Eager load courses
            .FirstOrDefault(u => u.UserId == userId);

                if (user == null)
                {
                    return null; // Or handle appropriately (throw exception, return empty list, etc.)
                }

                return user.Courses.ToList();
        }


        public int CountEnrollCoursesByUserId(Guid userId)
        {
            var user = _theDbContext.Users
            .Include(u => u.Courses) // Eager load courses
            .FirstOrDefault(u => u.UserId == userId);

            if (user == null)
            {
                return 0; // Or handle appropriately (throw exception, return empty list, etc.)
            }

            return user.Courses.ToList().Count;
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
