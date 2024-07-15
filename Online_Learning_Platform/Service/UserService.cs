using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Online_Learning_Platform.AllDbContext;
using Online_Learning_Platform.DTOs.ResuestDTO;
using Online_Learning_Platform.Interfaces;
using Online_Learning_Platform.Model;
using Online_Learning_Platform.RepositoryInterface;
using Online_Learning_Platform.Validation;
using System.Text;

namespace Online_Learning_Platform.Service
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IEnrollmentService _enrollmentService;
        private readonly IReviewService _reviewService;

        public UserService( 
            IMapper mapper,
            IUserRepository userRepository,
            IEnrollmentService enrollmentService,
            IReviewService reviewService)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _enrollmentService = enrollmentService;
            _reviewService = reviewService;
        }


        

        public string Register(UserRegistrationRequestDTO userRegistrationRequestDTO)
        {
            var validator = new UserRegistrationValidator();
            var res = validator.Validate(userRegistrationRequestDTO);

            if(!res.IsValid)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in res.Errors)
                {
                    //Console.WriteLine(failure.ErrorMessage);
                    sb.Append(failure.ErrorMessage +" ");
                }
                throw new Exception(sb.ToString());
            }

            var user = _mapper.Map<User>(userRegistrationRequestDTO);
            user.UserId = Guid.NewGuid();

            //_theDbContext.Users.Add(user);
            //_theDbContext.SaveChanges();
            _userRepository.SaveToUsersDb(user);
            _userRepository.Save();

            return $"User is registered in the database with id {user.UserId}";
        }


        public string DeleteUserById(Guid userId)
        {
            //find user and include the enrollments and reviews
            User? user = _userRepository
                .FindUserByIdAndIncludeReviewsAndEnrollments(userId);

            if (user == null)
            {
                throw new Exception("User is not found");
            }

            //delete the list of enrollments - just unenroll them
            foreach (var enrollment in user.Enrollments.ToList())
            {
                _enrollmentService.UnEnroll(enrollment.EnrollmentId);
            }

            //then just delete all the reviews
            foreach (var review in user.Reviews.ToList())
            {
                _reviewService.DeleteReview(review.ReviewId);
            }

            _userRepository.RemoveUser(user);
            _userRepository.Save();

            return "Successfully deleted the user with id " + userId;
        }


        public string UpdateUserProfile(UserProfileUpdateRequestDTO userProfileUpdateRequestDTO)
        {
            var user= _userRepository
                .FindUserById(userProfileUpdateRequestDTO.UserId);

            if (user == null)
            {
                throw new Exception("Not Found");
            }

            _mapper.Map(userProfileUpdateRequestDTO, user);

            //_theDbContext.Users.Update(user);
            _userRepository.Save();

            return "User details got successfully updated";
        }


        public List<Course> GetCourseListForUserById(Guid userId)
        {
            User user = _userRepository
                .FindUserByIdIncludeEnrollmentsAndCourses(userId);

            if(user==null)
            {
                throw new Exception("User not found in our system");
            }

            List<Course> courseList = user.Enrollments
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
            var user = _userRepository.FindUserByIdIncludeReviews(userId);


            if (user == null)
            {
                throw new Exception("User is not exist");
            }
            
            return user.Reviews.Count;
        }
    }
}
