using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Online_Learning_Platform.AllDbContext;
using Online_Learning_Platform.DTOs.ResuestDTO;
using Online_Learning_Platform.Enums;
using Online_Learning_Platform.Interfaces;
using Online_Learning_Platform.Model;
using Online_Learning_Platform.RepositoryInterface;
using Online_Learning_Platform.Validation;
using System.Text;

namespace Online_Learning_Platform.Service
{
    public class CourseService : ICourseService
    {
        private readonly IMapper _mapper;
        private readonly IEnrollmentService _enrollmentService;
        private readonly IInstructorService _instructorService;
        private readonly IReviewService _reviewService;
        private readonly ICourseRepository _courseRepository;

        private readonly IHttpClientFactory _httpClientFactory;


        public CourseService(
            IMapper mapper,
            IEnrollmentService enrollmentService,
            IInstructorService instructorService,
            IReviewService reviewService,
            ICourseRepository courseRepository,
            IHttpClientFactory httpClientFactory)
        {
            
            _mapper = mapper;
            _enrollmentService = enrollmentService;
            _instructorService = instructorService;
            _reviewService = reviewService;
            _courseRepository = courseRepository;
            _httpClientFactory = httpClientFactory;
        }


        
        public void CourseValidationChecking(Course course)
        {
            if (course == null)
            {
                throw new ArgumentNullException(nameof(course), "Course object cannot be null");
            }

            var validator = new CourseValidator();
            var res = validator.Validate(course);

            StringBuilder sb = new StringBuilder();

            if (!res.IsValid)
            {
                foreach (var failure in res.Errors)
                {
                    Console.WriteLine(failure.ErrorMessage);
                    sb.AppendLine(failure.ErrorMessage);
                }
                throw new Exception(sb.ToString());
            }
        }

        public string AddNewCourse(Course course)
        {
            CourseValidationChecking(course);

            //if (course == null)
            //{
            //    return "Not Possible";
            //}

            //User user = _allTheDbContext.Users.Find(course.UserId); 

            Course newCourse = new Course
            {
                CourseId = Guid.NewGuid(),
                CourseName = course.CourseName,
                CourseDescription = course.CourseDescription,
                Category = course.Category,
                DifficultyLevel = course.DifficultyLevel,
                Price = course.Price,
                StartDate = course.StartDate,
                EndDate = course.EndDate, 

            };

            //_allTheDbContext.Courses.Add(newCourse);
            //_allTheDbContext.SaveChanges(); 
            _courseRepository.SaveToCourseDb(newCourse);
            _courseRepository.Save();
                    
            return $"{newCourse.CourseName} is added";


        }


        public List<Course> GetAllCourses()
        {
            List<Course> courses =_courseRepository.GetAllCourses();
            return courses;
        }


        public Course GetCourseByCourseId(Guid courseId)
        {
            var course = _courseRepository.FindCourseById(courseId);

            if(course == null)
            {
                throw new Exception($"this id {courseId} does not exist in our system");
            }
            return course;
        }


        public async Task<string> RemoveCourseById(Guid courseId)
        {
            var course = _courseRepository
             .FindCourseByIdAndIncludeEnrollmentsAndUsersAndReviewsAndInstructors(courseId);

            //if (course==null)
            //{
            //    return "Not Found";
            //}

            CourseValidationChecking(course);

            //remove all the reviews
            foreach (var review in course.Reviews.ToList())
            {
                _reviewService.DeleteReview(review.ReviewId);
            }

            //remove all instructor in instructors list
            foreach (var instructor in course.Instructors.ToList())
            {
                instructor.Course = null;
            }

            // unenroll all the enrollments
            foreach (var enrollment in course.Enrollments.ToList())
            {
                //  _enrollmentService.UnEnroll(enrollment.EnrollmentId);
                HttpClient client = _httpClientFactory.CreateClient();

                string uri = $"http://localhost:8080/unenroll?enrollmentId={enrollment.EnrollmentId}";

                HttpResponseMessage response = await client
                    .GetAsync(uri);
            }

            


            //remove all the user
            course.Users.Clear();


            //_allTheDbContext.Courses.Remove(course);
            //_allTheDbContext.SaveChanges();
            _courseRepository.DeleteCourse(course);
            _courseRepository.Save();

            return "Course is successfully removed";
        }


        public string UpdateCourseDetails(CourseDetailsUpdateDTO courseDetailsUpdateDto)
        {
            var course = _courseRepository
                .FindCourseById(courseDetailsUpdateDto.CourseId);


            //if(course==null)
            //{
            //    return "Not Found";
            //}
            CourseValidationChecking(course);

            _mapper.Map(courseDetailsUpdateDto, course);

            _courseRepository.Save();

            return "Courser details updated successfully";
        }


        public int GetNoOfReviewsByCourseId(Guid courseId)
        {
            var course = _courseRepository
                .FindCourseByIdAndIncludeReviews(courseId);

            //if (course == null)
            //{
            //    throw new Exception("Course is not exist");
            //}
            CourseValidationChecking(course);

            return course.Reviews.Count;
        }

        public int GetNoOfEnrollmentsByCourseId(Guid courseId)
        {
            var course = _courseRepository
                .FindCourseByIdAndIncludeEnrollments(courseId);

            //if (course == null)
            //{
            //    throw new Exception("Course is not exist");
            //}
            CourseValidationChecking(course);

            return course.Enrollments.Count;
        }

        public List<string>GetAllEnrollmentsByCourseId(Guid courseId)
        {
            // var course = _allTheDbContext.Courses.Find(courseId);
            var course = _courseRepository
              .FindCourseByIdAndIncludeEnrollmentsAndIncludeUserFromEnrollmentTable(courseId);

            CourseValidationChecking(course);

            //if (course==null)
            //{
            //    //throw new Exception("Course does not exist");
            //    return Tuple.Create<List<string>, string>(null, "Course does not exist");
            //}

            //List<User>usersList = _allTheDbContext.StudentCourses
            //    .Include(x => x.User)
            //    .Where(x => x.CourseId==courseId)
            //    .Select(x => x.User)
            //    .ToList()!;

            //find enrollments
            var enrollments = course.Enrollments;

            if(enrollments==null || enrollments.Count==0)
            {
                //return Tuple.Create<List<string>, string>(null, "No enrollments found");
                throw new Exception("No enrollments found");
            }

            //find the users from the enrollments

            List<User> usersList = [];

            foreach (var enrollment in enrollments)
            {
                usersList.Add(enrollment.User);
            }


            if(usersList.Count==0)
            {
                //throw new Exception("No users found");
               // return Tuple.Create<List<string>, string>(null, "No users found");
               throw new Exception("No users found");
            }

            List<string>names = new List<string>();

            foreach (var user in usersList)
            {
                names.Add(user.UserName!);
            }

            return names;
            //return Tuple.Create<List<string>, string>(names, "Users are found");
        }

        public void Hi()
        {
            Console.WriteLine("Hi Rohan! I am in Hi method in Course Service");
        }
    }
}
