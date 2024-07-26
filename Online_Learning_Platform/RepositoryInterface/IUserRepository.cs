using Online_Learning_Platform.Model;

namespace Online_Learning_Platform.RepositoryInterface
{
    public interface IUserRepository
    {
        public User GetUserByUserIdAndIncludesEnrollmentsAndCourses(Guid userId);
        public void Save();
        public void SaveToUsersDb(User user);
        public User FindUserById(Guid userId);
        public void RemoveUser(User user);
        public User FindUserByIdAndIncludeReviewsAndEnrollments(Guid userId);
        public User FindUserByIdIncludeEnrollmentsAndCourses(Guid userId);
        public User FindUserByIdIncludeReviews(Guid userId);
        public User? FindUserByEmail(string email);
    }
}
