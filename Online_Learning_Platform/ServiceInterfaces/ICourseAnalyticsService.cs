using Online_Learning_Platform.Enums;

namespace Online_Learning_Platform.Interfaces
{
    public interface ICourseAnalyticsService
    {
        public decimal CalculateTotalRevenueByCourseId(Guid courseId);
        public int CountNoOfOngoingCourses(Progress progress);
    }
}
