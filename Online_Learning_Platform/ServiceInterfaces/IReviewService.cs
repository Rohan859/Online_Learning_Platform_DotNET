using Online_Learning_Platform.DTOs;

namespace Online_Learning_Platform.Interfaces
{
    public interface IReviewService
    {
        public string SubmitReview(ReviewRequestDTO reviewRequestDTO);
        public string UpdateReview(Guid reviewId, string description);
        public string DeleteReview(Guid reviewId);
    }
}
