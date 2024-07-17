using Microsoft.EntityFrameworkCore;
using Online_Learning_Platform.AllDbContext;
using Online_Learning_Platform.Enums;
using Online_Learning_Platform.Model;
using Online_Learning_Platform.RepositoryInterface;

namespace Online_Learning_Platform.Repository
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly AllTheDbContext _dbContext;

        public EnrollmentRepository(AllTheDbContext allTheDbContext)
        {
            _dbContext = allTheDbContext;
        }

        public void AddToEnrollmentDb(Enrollment enrollment)
        {
            _dbContext.Enrollments.Add(enrollment);
        }

        public async Task AddToEnrollmentDbAsAsync(Enrollment enrollment)
        {
            await _dbContext.Enrollments.AddAsync(enrollment);
            await _dbContext.SaveChangesAsync();

        }

        public int CountNoOfProgress(Progress progress)
        {
            var noOfOngoingCourses = _dbContext.Enrollments
                .Count(e => e.Progress == progress);

            return noOfOngoingCourses;
        }

        public void Delete(Enrollment enrollment)
        {
            _dbContext.Enrollments.Remove(enrollment);
        }

        public Enrollment GetEnrollmentByEnrollmentIdAndIncludeTheUserTheCourseAndTheUsersofCourse(Guid enrollmentId)
        {
            var enrollment = _dbContext.Enrollments
                                .Include(e => e.Course)
                                .ThenInclude(e => e!.Users)
                                .Include(e => e.User)
                                .FirstOrDefault(e => e.EnrollmentId == enrollmentId);

            return enrollment;
        }

        public bool IsAlreadyEnrolled(Guid userId, Guid courseId)
        {
            bool isAlreadyEnrolled = _dbContext.Enrollments
               .Any(e => e.UserId == userId && e.CourseId == courseId);

            return isAlreadyEnrolled;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public async Task SaveAsAsync()
        {
           await _dbContext.SaveChangesAsync();
        }

        public List<Enrollment> TrackProgress(Progress progress)
        {
            var list = _dbContext.Enrollments
              .Where(e => e.Progress == progress)
              .ToList();

            return list;
        }
    }
}
