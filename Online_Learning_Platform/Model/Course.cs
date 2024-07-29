using Online_Learning_Platform.Enums;
using System.ComponentModel.DataAnnotations;

namespace Online_Learning_Platform.Model
{
    public class Course
    {
        
        public Guid CourseId { get; set; }
        public string? CourseName { get; set; }
        public string? CourseDescription { get; set; }
        public Category Category { get; set; }
        public DifficultyLevel DifficultyLevel { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        public List<Instructor> Instructors { get; set; }= new List<Instructor>();


        public List<Review> Reviews { get; set; } =new List<Review>();

        public List<Enrollment> Enrollments { get; set; }=new List<Enrollment>();

        public List<User> Users {  get; set; }=new List<User>();
        public string Role { get; } = "Admin";
    }
}
