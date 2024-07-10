using Online_Learning_Platform.Enums;
using Online_Learning_Platform.Model;

namespace Online_Learning_Platform.RepositoryInterface
{
    public interface IEnrollmentRepository 
    {
        public bool IsAlreadyEnrolled(Guid userId, Guid courseId);
        public void AddToEnrollmentDb(Enrollment enrollment);
        public Enrollment GetEnrollmentByEnrollmentIdAndIncludeTheUserTheCourseAndTheUsersofCourse
            (Guid enrollmentId);
        public void Save();
        public void Delete(Enrollment enrollment);

        public List<Enrollment> TrackProgress(Progress progress);
    }
}
