using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Online_Learning_Platform.AllDbContext;
using Online_Learning_Platform.Model;
using Online_Learning_Platform.RepositoryInterface;

namespace Online_Learning_Platform.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AllTheDbContext _dbContext;
        private readonly IMemoryCache _cache;

        public UserRepository(AllTheDbContext allTheDbContext,
            IMemoryCache cache)
        {
            _dbContext = allTheDbContext;
            _cache = cache;
        }

        public User FindUserById(Guid userId)
        {         
            return _cache.GetOrCreate($"User_{userId}", entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);

                //db query if not available in cache
                User? user = _dbContext.Users.Find(userId);
                return user;

            })!;
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
            //using caching for better retrival of data
            return _cache.GetOrCreate($"User_{userId}", entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);

                //database query to get the user from db
                var user = _dbContext.Users
               .Include(x => x.Enrollments)
               .ThenInclude(x => x.Course)
               .FirstOrDefault(x => x.UserId == userId);

                return user;
            })!;
           
        }

        public User FindUserByIdIncludeReviews(Guid userId)
        {
            //var user = _dbContext.Users
            //   .Include(u => u.Reviews)
            //   .FirstOrDefault(u => u.UserId == userId);

            //return user;

            return _cache.GetOrCreate($"User_{userId}", entry =>
            {
                //cache is valid for only 5 minutes
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);

                var user = _dbContext.Users
                   .Include(u => u.Reviews)
                   .FirstOrDefault(u => u.UserId == userId);

                return user;
            })!;
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
