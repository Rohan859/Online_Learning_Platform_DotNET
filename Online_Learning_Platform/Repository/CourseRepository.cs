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
    }
}
