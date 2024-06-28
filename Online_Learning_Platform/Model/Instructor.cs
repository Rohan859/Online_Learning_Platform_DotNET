using Online_Learning_Platform.Enums;
using System.ComponentModel.DataAnnotations;

namespace Online_Learning_Platform.Model
{
    public class Instructor
    {
        [Key]
        public Guid InstructorId { get; set; }

        public string? InstructorName { get; set; }
        public string? Email { get; set; }
        public string? MobileNo { get;set; }
        public string? Password { get; set; }
        public Expertise Expertise { get; set; }
        public decimal Salary {  get; set; }
        public string? Description {  get; set; }

        public Course? Course { get; set; }
        public Guid? CourseId { get; set; }
    }
}
