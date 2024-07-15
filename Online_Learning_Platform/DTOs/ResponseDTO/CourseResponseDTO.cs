using Online_Learning_Platform.Model;

namespace Online_Learning_Platform.DTOs.ResponseDTO
{
    public class CourseResponseDTO
    {
        public string? Message {  get; set; }
        public Course? Course { get; set; }
    }
}
