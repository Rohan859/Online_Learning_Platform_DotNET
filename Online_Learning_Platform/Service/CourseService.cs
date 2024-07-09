using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Online_Learning_Platform.AllDbContext;
using Online_Learning_Platform.DTOs;
using Online_Learning_Platform.Enums;
using Online_Learning_Platform.Interfaces;
using Online_Learning_Platform.Model;

namespace Online_Learning_Platform.Service
{
    public class CourseService : ICourseService
    {
        private readonly AllTheDbContext _allTheDbContext;
        private readonly IMapper _mapper;

        public CourseService(AllTheDbContext context,IMapper mapper)
        {
            _allTheDbContext = context;
            _mapper = mapper;
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

            _allTheDbContext.Courses.Add(newCourse);
            _allTheDbContext.SaveChanges(); 
            
            
            return $"{newCourse.CourseName} is added";


        }


        public List<Course> GetAllCourses()
        {
            List<Course> courses = _allTheDbContext.Courses.ToList();
            return courses;
        }


        public Course? GetCourseByCourseId(Guid courseId)
        {
            var course = _allTheDbContext.Courses.Find(courseId);
            return course;
        }


        public string RemoveCourseById(Guid courseId)
        {
            var course = _allTheDbContext.Courses.Find(courseId);

            if(course==null)
            {
                return "Not Found";
            }
            _allTheDbContext.Courses.Remove(course);
            _allTheDbContext.SaveChanges();

            return "Course is successfully removed";
        }


        public string UpdateCourseDetails(CourseDetailsUpdateDTO courseDetailsUpdateDto)
        {
            var course = _allTheDbContext.Courses.Find(courseDetailsUpdateDto.CourseId);

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

            _allTheDbContext.SaveChanges();

            return "Courser details updated successfully";
        }


        public int GetNoOfReviewsByCourseId(Guid courseId)
        {
            var course = _allTheDbContext.Courses
                .Include(e => e.Reviews)
                .FirstOrDefault(e => e.CourseId==courseId);
              


            if (course == null)
            {
                throw new Exception("Course is not exist");
            }

            return course.Reviews.Count;
        }

        public int GetNoOfEnrollmentsByCourseId(Guid courseId)
        {
            var course = _allTheDbContext.Courses
                .Include (e => e.Enrollments)
                .FirstOrDefault(e => e.CourseId==courseId);

            if(course == null)
            {
                throw new Exception("Course is not exist");
            }

            return course.Enrollments.Count;
        }

        public Tuple<List<string>,string>GetAllEnrollmentsByCourseId(Guid courseId)
        {
            // var course = _allTheDbContext.Courses.Find(courseId);
            var course = _allTheDbContext.Courses
                 .Include(e => e.Enrollments)
                 .ThenInclude(e => e.User)
                 .FirstOrDefault(x => x.CourseId == courseId);

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
