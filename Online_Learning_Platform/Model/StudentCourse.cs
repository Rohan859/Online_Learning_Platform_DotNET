namespace Online_Learning_Platform.Model
{
    public class StudentCourse
    {
        public Guid UserId {  get; set; }
        public User? User { get; set; }

        public Guid CourseId { get; set; }

        public Course? Course { get; set; }  
    }
}
