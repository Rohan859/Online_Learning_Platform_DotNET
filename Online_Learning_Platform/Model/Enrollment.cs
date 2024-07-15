using Online_Learning_Platform.Enums;
using System.ComponentModel.DataAnnotations;

namespace Online_Learning_Platform.Model
{
    public class Enrollment
    {
       
        public Guid EnrollmentId { get; set; }
        public DateTime EnrollmentDate {  get; set; }
        public Progress Progress { get; set; }
        public Course? Course { get; set; }
        public Guid? CourseId { get; set; } 

        public Guid UserId { get; set; }
        public User? User { get; set; }
    }
}
