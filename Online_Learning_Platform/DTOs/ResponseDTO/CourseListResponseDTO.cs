using Online_Learning_Platform.Model;

namespace Online_Learning_Platform.DTOs.ResponseDTO
{
    public class CourseListResponseDTO
    {
        public string? Message { get; set; }
        public List<Course>? Courses { get; set; }
    }
}
