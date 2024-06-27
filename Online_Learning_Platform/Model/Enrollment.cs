using Online_Learning_Platform.Enums;
using System.ComponentModel.DataAnnotations;

namespace Online_Learning_Platform.Model
{
    public class Enrollment
    {
        [Key]
        public Guid EnrollmentId { get; set; }
        public DateTime EnrollmentDate {  get; set; }
        public Progress Progress { get; set; }
        public User? User { get; set; }
    }
}
