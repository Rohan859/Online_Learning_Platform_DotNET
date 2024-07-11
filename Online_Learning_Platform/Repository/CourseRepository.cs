using Microsoft.EntityFrameworkCore;
using Online_Learning_Platform.AllDbContext;
using Online_Learning_Platform.Model;
using Online_Learning_Platform.RepositoryInterface;

namespace Online_Learning_Platform.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly AllTheDbContext _dbContext;

        public CourseRepository(AllTheDbContext allTheDbContext)
        {
            _dbContext = allTheDbContext;
        }

        public void DeleteCourse(Course course)
        {
            _dbContext.Courses.Remove(course);
        }

        public Course? FindCourseById(Guid courseId)
        {
            var course = _dbContext.Courses.Find(courseId);
            return course;
        }

        public Course? FindCourseByIdAndIncludeEnrollments(Guid courseId)
        {
            var course = _dbContext.Courses
                .Include(e => e.Enrollments)
                .FirstOrDefault(e => e.CourseId == courseId);

            return course;
        }

        public Course? FindCourseByIdAndIncludeEnrollmentsAndIncludeUserFromEnrollmentTable(Guid courseId)
        {
            var course = _dbContext.Courses
                .Include(e => e.Enrollments)
                .ThenInclude(e => e.User)
                .FirstOrDefault(x => x.CourseId == courseId);

            return course;
        }

        public Course? FindCourseByIdAndIncludeEnrollmentsAndUsersAndReviewsAndInstructors(Guid courseId)
        {
            var course = _dbContext.Courses
               .Include(x => x.Instructors)
               .Include(x => x.Reviews)
               .Include(x => x.Enrollments)
               .Include(x => x.Users)
               .FirstOrDefault(x => x.CourseId == courseId);

            return course;
        }

        public Course? FindCourseByIdAndIncludeReviews(Guid courseId)
        {
            var course = _dbContext.Courses
                .Include(e => e.Reviews)
                .FirstOrDefault(e => e.CourseId == courseId);

            return course;
        }

        public List<Course> GetAllCourses()
        {
            List<Course> courses = _dbContext.Courses.ToList();
            return courses;
        }

        public Course GetCourseByCourseIdAndIncludesEnrollmentsAndUsers(Guid courseId)
        {
            var course = _dbContext.Courses
                .Include(e => e.Enrollments)
                .Include(x => x.Users)
                .FirstOrDefault(e => e.CourseId == courseId);

            return course;
        }

        public void Save()
        {
           _dbContext.SaveChanges();
        }

        public void SaveToCourseDb(Course course)
        {
            _dbContext.Courses.Add(course);
        }
    }
}
