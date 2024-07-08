using Online_Learning_Platform.Enums;

namespace Online_Learning_Platform.DTOs
{
    public class InstructorUpdateRequestDTO
    {
        public Guid InstructorId { get; set; }
        public string? InstructorName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        //public Expertise Expertise { get; set; }
        public string? Description { get; set; }
    }
}
