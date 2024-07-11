using Online_Learning_Platform.Enums;
using Online_Learning_Platform.Model;

namespace Online_Learning_Platform.Interfaces
{
    public interface IEnrollmentService
    {
        public string EnrollInACourse(Guid userId, Guid courseId);
        public string UnEnroll(Guid enrollmentId);
       // public string DeleteEnrollment(Guid enrollmentId);
        public List<Enrollment> TrackProgress(Progress progress);
    }
}
