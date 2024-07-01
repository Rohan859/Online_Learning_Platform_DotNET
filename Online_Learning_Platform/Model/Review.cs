using System.ComponentModel.DataAnnotations;

namespace Online_Learning_Platform.Model
{
    public class Review
    {
        [Key]
        public Guid ReviewId { get; set; }
        public string? Description {  get; set; }
        public User? User { get; set; }
        public Guid UserId { get; set; }
        public Course? Course { get; set; }
        public Guid CourseId { get; set; }
    }
}
