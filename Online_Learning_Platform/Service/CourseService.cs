using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Online_Learning_Platform.AllDbContext;
using Online_Learning_Platform.DTOs;
using Online_Learning_Platform.Enums;
using Online_Learning_Platform.Interfaces;
using Online_Learning_Platform.Model;
using Online_Learning_Platform.RepositoryInterface;

namespace Online_Learning_Platform.Service
{
    public class CourseService : ICourseService
    {
        private readonly IMapper _mapper;
        private readonly IEnrollmentService _enrollmentService;
        private readonly IInstructorService _instructorService;
        private readonly IReviewService _reviewService;
        private readonly ICourseRepository _courseRepository;

        public CourseService(
            IMapper mapper,
            IEnrollmentService enrollmentService,
            IInstructorService instructorService,
            IReviewService reviewService,
            ICourseRepository courseRepository)
        {
            
            _mapper = mapper;
            _enrollmentService = enrollmentService;
            _instructorService = instructorService;
            _reviewService = reviewService;
            _courseRepository = courseRepository;
        }

        public string AddNewCourse(Course course)
        {
            if(course == null)
            {
                return "Not Possible";
            }

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


        public Course? GetCourseByCourseId(Guid courseId)
        {
            var course = _courseRepository.FindCourseById(courseId);
            return course;
        }


        public string RemoveCourseById(Guid courseId)
        {
            var course = _courseRepository
             .FindCourseByIdAndIncludeEnrollmentsAndUsersAndReviewsAndInstructors(courseId);

            if (course==null)
            {
                return "Not Found";
            }

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
                _enrollmentService.UnEnroll(enrollment.EnrollmentId);
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


            if(course==null)
            {
                return "Not Found";
            }

            //var courseName = courseDetails.CourseName;
            //var description = courseDetails.CourseDescription;
            //var startDate = courseDetails.StartDate;
            //var endDate = courseDetails.EndDate;
            //var price = courseDetails.Price;


            //if(courseName!=null)
            //{
            //    course.CourseName= courseName;
            //}

            //if(description!=null)
            //{
            //    course.CourseDescription= description;
            //}

            //if(startDate!=new DateTime())
            //{
            //   course.StartDate=startDate;
            //}

            //if (endDate != new DateTime())
            //{
            //    course.EndDate = endDate;
            //}

            //if(price>0)
            //{
            //    course.Price= price;
            //}

            _mapper.Map(courseDetailsUpdateDto, course);

            _courseRepository.Save();

            return "Courser details updated successfully";
        }


        public int GetNoOfReviewsByCourseId(Guid courseId)
        {
            var course = _courseRepository
                .FindCourseByIdAndIncludeReviews(courseId);

            if (course == null)
            {
                throw new Exception("Course is not exist");
            }

            return course.Reviews.Count;
        }

        public int GetNoOfEnrollmentsByCourseId(Guid courseId)
        {
            var course = _courseRepository
                .FindCourseByIdAndIncludeEnrollments(courseId);

            if (course == null)
            {
                throw new Exception("Course is not exist");
            }

            return course.Enrollments.Count;
        }

        public Tuple<List<string>,string>GetAllEnrollmentsByCourseId(Guid courseId)
        {
            // var course = _allTheDbContext.Courses.Find(courseId);
            var course = _courseRepository
                .FindCourseByIdAndIncludeEnrollmentsAndIncludeUserFromEnrollmentTable(courseId);

            if (course==null)
            {
                //throw new Exception("Course does not exist");
                return Tuple.Create<List<string>, string>(null, "Course does not exist");
            }

            //List<User>usersList = _allTheDbContext.StudentCourses
            //    .Include(x => x.User)
            //    .Where(x => x.CourseId==courseId)
            //    .Select(x => x.User)
            //    .ToList()!;

            //find enrollments
            var enrollments = course.Enrollments;

            if(enrollments==null || enrollments.Count==0)
            {
                return Tuple.Create<List<string>, string>(null, "No enrollments found");
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
                return Tuple.Create<List<string>, string>(null, "No users found");
            }

            List<string>names = new List<string>();

            foreach (var user in usersList)
            {
                names.Add(user.UserName!);
            }

            //return names;
            return Tuple.Create<List<string>, string>(names, "Users are found");
        }

    }
}
