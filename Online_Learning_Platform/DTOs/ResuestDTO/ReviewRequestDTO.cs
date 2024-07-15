namespace Online_Learning_Platform.DTOs.ResuestDTO
{
    public class ReviewRequestDTO
    {
        public Guid UserId { get; set; }
        public Guid CourseId { get; set; }
        public string? Description { get; set; }
    }
}
