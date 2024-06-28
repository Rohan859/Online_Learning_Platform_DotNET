using Online_Learning_Platform.AllDbContext;
using Online_Learning_Platform.DTOs;
using Online_Learning_Platform.Enums;
using Online_Learning_Platform.Model;

namespace Online_Learning_Platform.Service
{
    public class CourseService
    {
        private readonly AllTheDbContext _allTheDbContext;

        public CourseService(AllTheDbContext context)
        {
            _allTheDbContext = context;
        }

        public string AddNewCourse(Course course)
        {
            if(course == null)
            {
                return "Not Possible";
            }

            User user = _allTheDbContext.Users.Find(course.UserId);

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


        public string UpdateCourseDetails(CourseDetailsUpdateDTO courseDetails)
        {
            var course = _allTheDbContext.Courses.Find(courseDetails.CourseId);

            if(course==null)
            {
                return "Not Found";
            }

            var courseName = courseDetails.CourseName;
            var description = courseDetails.CourseDescription;
            var startDate = courseDetails.StartDate;
            var endDate = courseDetails.EndDate;
            var price = courseDetails.Price;


            if(courseName!=null)
            {
                course.CourseName= courseName;
            }

            if(description!=null)
            {
                course.CourseDescription= description;
            }

            if(startDate!=new DateTime())
            {
               course.StartDate=startDate;
            }

            if (endDate != new DateTime())
            {
                course.EndDate = endDate;
            }

            if(price>0)
            {
                course.Price= price;
            }

            _allTheDbContext.SaveChanges();

            return "Courser details updated successfully";
        }
    }
}
