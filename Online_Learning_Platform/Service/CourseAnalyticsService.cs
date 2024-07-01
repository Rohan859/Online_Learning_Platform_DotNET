using Microsoft.EntityFrameworkCore;
using Online_Learning_Platform.AllDbContext;
using Online_Learning_Platform.Enums;

namespace Online_Learning_Platform.Service
{
    public class CourseAnalyticsService
    {
        private readonly AllTheDbContext _theDbContext;

        public CourseAnalyticsService(AllTheDbContext allTheDbContext)
        {
            _theDbContext = allTheDbContext;
        }

        public decimal CalculateTotalRevenueByCourseId(Guid courseId)
        {
            var course = _theDbContext.Courses
                           .Include(c => c.Enrollments)
                           .FirstOrDefault(c => c.CourseId == courseId);


            if (course == null)
            {
                throw new Exception("Course is not exist in our system");
            }

            // Ensure Enrollments are loaded
            _theDbContext.Entry(course).Collection(c => c.Enrollments).Load();

            return course.Enrollments.Count() * course.Price;
        }


        public int CountNoOfOngoingCourses(Progress progress)
        {
            var noOfOngoingCourses = _theDbContext.Enrollments
                .Count(e => e.Progress == progress);

            return noOfOngoingCourses;
        }
    }
}
