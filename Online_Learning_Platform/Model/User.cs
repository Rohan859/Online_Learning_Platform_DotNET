using System.ComponentModel.DataAnnotations;

namespace Online_Learning_Platform.Model
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }

        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? MobileNo {  get; set; }

        public List<Course> Courses { get; set; }=new List<Course>();
        

        public List<Review> Reviews { get; set; }=new List<Review>();
    }
}
