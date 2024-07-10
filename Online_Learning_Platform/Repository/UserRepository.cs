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
        public User GetUserByUserIdAndIncludesEnrollmentsAndCourses(Guid userId)
        {
            var user = _dbContext.Users
               .Include(x => x.Enrollments)
               .Include(x => x.Courses)
               .FirstOrDefault(x => x.UserId == userId);

            return user;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
