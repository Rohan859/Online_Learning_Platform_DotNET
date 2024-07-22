using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Online_Learning_Platform.AllDbContext;
using Online_Learning_Platform.Model;
using Online_Learning_Platform.RepositoryInterface;

namespace Online_Learning_Platform.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly AllTheDbContext _dbContext;
        private readonly IMemoryCache _cache;

        public CourseRepository(AllTheDbContext allTheDbContext,
            IMemoryCache cache)
        {
            _dbContext = allTheDbContext;
            _cache = cache;
        }

        public void DeleteCourse(Course course)
        {
            _dbContext.Courses.Remove(course);
        }

        public Course? FindCourseById(Guid courseId)
        {
            //var course = _dbContext.Courses.Find(courseId);
            //return course;

            return _cache.GetOrCreate($"Course_{courseId}", entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);

                var course = _dbContext.Courses.Find(courseId);
                return course;
            });
        }

        public Course? FindCourseByIdAndIncludeEnrollments(Guid courseId)
        {
            return _cache.GetOrCreate($"Course_{courseId}", entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);

                var course = _dbContext.Courses
                .Include(e => e.Enrollments)
                .FirstOrDefault(e => e.CourseId == courseId);

                return course;
            });

            //var course = _dbContext.Courses
            //    .Include(e => e.Enrollments)
            //    .FirstOrDefault(e => e.CourseId == courseId);

            //return course;
        }

        public Course? FindCourseByIdAndIncludeEnrollmentsAndIncludeUserFromEnrollmentTable(Guid courseId)
        {

            return _cache.GetOrCreate($"Course_{courseId}", entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);

               var course = _dbContext.Courses
                .Include(e => e.Enrollments)
                .ThenInclude(e => e.User)
                .FirstOrDefault(x => x.CourseId == courseId);

            return course;
            });
            //var course = _dbContext.Courses
            //    .Include(e => e.Enrollments)
            //    .ThenInclude(e => e.User)
            //    .FirstOrDefault(x => x.CourseId == courseId);

            //return course;
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

        public Course? FindCourseByIdAndIncludeInstructors(Guid courseId)
        {
            var course = _dbContext.Courses
                 .Include(e => e.Instructors)
                 .FirstOrDefault(e => e.CourseId == courseId);

            return course;
        }

        public Course? FindCourseByIdAndIncludeReviews(Guid courseId)
        {
            return _cache.GetOrCreate($"Course_{courseId}", entry =>
            {
                //cache is valid for 5 minutes only
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);

                //search from db
                var course = _dbContext.Courses
                .Include(e => e.Reviews)
                .FirstOrDefault(e => e.CourseId == courseId);

                return course;

            });          
        }

        public List<Course> GetAllCourses()
        {            
            if(!_cache.TryGetValue<List<Course>>("AllCourses",out var courses))
            {
                courses = _dbContext.Courses.ToList();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));


                _cache.Set("AllCourses",courses,cacheEntryOptions);
            }
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

        public void LoadEnrollmentsFromCourse(Course course)
        {
            _dbContext.Entry(course).Collection(c => c.Enrollments).Load();
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
