using Microsoft.EntityFrameworkCore;
using Online_Learning_Platform.AllDbContext;
using Online_Learning_Platform.Enums;
using Online_Learning_Platform.Interfaces;
using Online_Learning_Platform.RepositoryInterface;

namespace Online_Learning_Platform.Service
{
    public class CourseAnalyticsService : ICourseAnalyticsService
    {
        
        private readonly ICourseRepository _courseRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;

        public CourseAnalyticsService(
            ICourseRepository courseRepository,
            IEnrollmentRepository enrollmentRepository)
        {
            
            _courseRepository = courseRepository;
            _enrollmentRepository = enrollmentRepository;
        }

        public decimal CalculateTotalRevenueByCourseId(Guid courseId)
        {
            var course = _courseRepository
                .FindCourseByIdAndIncludeEnrollments(courseId);


            if (course == null)
            {
                throw new Exception("Course is not exist in our system");
            }

            // Ensure Enrollments are loaded
            //_theDbContext.Entry(course).Collection(c => c.Enrollments).Load();
            _courseRepository.LoadEnrollmentsFromCourse(course);

            return course.Enrollments.Count() * course.Price;
        }


        public int CountNoOfProgress(Progress progress)
        {
            var noOfCourses = _enrollmentRepository.CountNoOfProgress(progress);

            return noOfCourses;
        }
    }
}
