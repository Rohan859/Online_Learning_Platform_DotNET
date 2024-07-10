using Online_Learning_Platform.Model;

namespace Online_Learning_Platform.RepositoryInterface
{
    public interface ICourseRepository
    {
        public Course GetCourseByCourseIdAndIncludesEnrollmentsAndUsers(Guid courseId);
        public void Save();
    }
}
