using Online_Learning_Platform.Model;

namespace Online_Learning_Platform.DTOs.ResponseDTO
{
    public class InstructorListResponseDTO
    {
        public string? Message {  get; set; }
        public List<Instructor>?Instructors { get; set; }
    }
}
