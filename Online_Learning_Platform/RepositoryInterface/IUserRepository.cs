using Online_Learning_Platform.Model;

namespace Online_Learning_Platform.RepositoryInterface
{
    public interface IUserRepository
    {
        public User GetUserByUserIdAndIncludesEnrollmentsAndCourses(Guid userId);
        public void Save();
    }
}
