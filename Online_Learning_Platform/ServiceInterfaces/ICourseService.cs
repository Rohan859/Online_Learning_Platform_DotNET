using Online_Learning_Platform.DTOs;
using Online_Learning_Platform.Model;

namespace Online_Learning_Platform.Interfaces
{
    public interface ICourseService
    {
        public string AddNewCourse(Course course);
        public List<Course> GetAllCourses();
        public Course? GetCourseByCourseId(Guid courseId);
        public string RemoveCourseById(Guid courseId);
        public string UpdateCourseDetails(CourseDetailsUpdateDTO courseDetailsUpdateDto);
        public int GetNoOfReviewsByCourseId(Guid courseId);
        public int GetNoOfEnrollmentsByCourseId(Guid courseId);
        public List<string> GetAllEnrollmentsByCourseId(Guid courseId);

    }
}
