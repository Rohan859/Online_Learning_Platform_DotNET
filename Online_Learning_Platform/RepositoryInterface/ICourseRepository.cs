using Online_Learning_Platform.Model;

namespace Online_Learning_Platform.RepositoryInterface
{
    public interface ICourseRepository
    {
        public Course GetCourseByCourseIdAndIncludesEnrollmentsAndUsers(Guid courseId);
        public void Save();
        public void SaveToCourseDb(Course course);
        public void DeleteCourse(Course course);
        public List<Course> GetAllCourses();
        public Course? FindCourseById(Guid courseId);
        public Course? FindCourseByIdAndIncludeInstructors(Guid courseId);
        public Course? FindCourseByIdAndIncludeEnrollmentsAndUsersAndReviewsAndInstructors(Guid courseId);
        public Course? FindCourseByIdAndIncludeReviews(Guid courseId);
        public Course? FindCourseByIdAndIncludeEnrollments(Guid courseId);
        public Course? FindCourseByIdAndIncludeEnrollmentsAndIncludeUserFromEnrollmentTable(Guid courseId);
        public void LoadEnrollmentsFromCourse(Course course);
    }
}
