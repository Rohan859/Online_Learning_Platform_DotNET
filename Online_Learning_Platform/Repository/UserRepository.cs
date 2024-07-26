using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Online_Learning_Platform.AllDbContext;
using Online_Learning_Platform.Model;
using Online_Learning_Platform.RepositoryInterface;
using System.Text;

namespace Online_Learning_Platform.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AllTheDbContext _dbContext;
        private readonly IMemoryCache _cache;
        private readonly IDistributedCache _distributedCache;

        public UserRepository(AllTheDbContext allTheDbContext,
            IMemoryCache cache,
            IDistributedCache distributedCache)
        {
            _dbContext = allTheDbContext;
            _cache = cache;
            _distributedCache = distributedCache;
        }

        public User FindUserById(Guid userId)
        {
            var cachedUserData = _distributedCache.Get($"User_{userId}");

            if (cachedUserData != null)
            {
                var cachedUserJson = Encoding.UTF8.GetString(cachedUserData);
                return JsonConvert.DeserializeObject<User>(cachedUserJson)!;
            }

            //if user not found then fetch from db
            User? user = _dbContext.Users.Find(userId);

            if (user != null)
            {
                var userJson = JsonConvert.SerializeObject(user);

                var cacheOptions = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

                _distributedCache.Set($"User_{userId}",Encoding.UTF8.GetBytes(userJson), cacheOptions);
            }
            return user;

            //return _cache.GetOrCreate($"User_{userId}", entry =>
            //{
            //    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);

            //    //db query if not available in cache
            //    User? user = _dbContext.Users.Find(userId);
            //    return user;

            //})!;
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

        public User? FindUserByEmail(string email)
        {
            User? user = _dbContext.Users
                .FirstOrDefault(u => u.Email == email);

            return user;
        }
    }
}
