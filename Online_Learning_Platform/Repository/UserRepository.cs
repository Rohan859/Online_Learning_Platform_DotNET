using Microsoft.EntityFrameworkCore;
using Online_Learning_Platform.AllDbContext;
using Online_Learning_Platform.Model;
using Online_Learning_Platform.RepositoryInterface;

namespace Online_Learning_Platform.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AllTheDbContext _dbContext;

        public UserRepository(AllTheDbContext allTheDbContext)
        {
            _dbContext = allTheDbContext;
        }

        public User FindUserById(Guid userId)
        {
            User? user = _dbContext.Users.Find(userId);
            return user;
        }

        public User FindUserByIdAndIncludeReviewsAndEnrollments(Guid userId)
        {
            User? user = _dbContext.Users
                .Include(x => x.Reviews)
                .Include(x => x.Enrollments)
                .FirstOrDefault(x => x.UserId == userId);

            return user;
        }

        public User FindUserByIdIncludeEnrollmentsAndCourses(Guid userId)
        {
            var user = _dbContext.Users
                .Include(x => x.Enrollments)
                .ThenInclude(x => x.Course)
                .FirstOrDefault(x => x.UserId == userId);

            return user;
        }

        public User FindUserByIdIncludeReviews(Guid userId)
        {
            var user = _dbContext.Users
               .Include(u => u.Reviews)
               .FirstOrDefault(u => u.UserId == userId);

            return user;
        }

        public User GetUserByUserIdAndIncludesEnrollmentsAndCourses(Guid userId)
        {
            var user = _dbContext.Users
               .Include(x => x.Enrollments)
               .Include(x => x.Courses)
               .FirstOrDefault(x => x.UserId == userId);

            return user;
        }

        public void RemoveUser(User user)
        {
            _dbContext.Users.Remove(user);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void SaveToUsersDb(User user)
        {
            _dbContext.Users.Add(user);
        }
    }
}
