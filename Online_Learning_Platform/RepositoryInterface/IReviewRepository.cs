using Online_Learning_Platform.Model;

namespace Online_Learning_Platform.RepositoryInterface
{
    public interface IReviewRepository
    {
        public void Save();
        public void SaveToReviewDb(Review review);
        public void DeleteReview(Review review);
        public Review FindReviewById(Guid reviewId);
        public Review FindReviewByIdAndIncludeUserAndCourse(Guid reviewId);
    }
}
