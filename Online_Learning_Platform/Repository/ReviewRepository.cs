

using Microsoft.EntityFrameworkCore;
using Online_Learning_Platform.AllDbContext;
using Online_Learning_Platform.Model;
using Online_Learning_Platform.RepositoryInterface;

namespace Online_Learning_Platform.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly AllTheDbContext _theDbContext;

        public ReviewRepository(AllTheDbContext allTheDbContext)
        {
            _theDbContext = allTheDbContext;
        }

        public void DeleteReview(Review review)
        {
            _theDbContext.Reviews.Remove(review);
        }

        public Review? FindReviewById(Guid reviewId)
        {
            var review = _theDbContext.Reviews.Find(reviewId);
            return review;
        }

        public Review? FindReviewByIdAndIncludeUserAndCourse(Guid reviewId)
        {
            var review = _theDbContext.Reviews
                .Include(e => e.User)
                .Include(e => e.Course)
                .FirstOrDefault(e => e.ReviewId == reviewId);

            return review;
        }

        public void Save()
        {
            _theDbContext.SaveChanges();
        }

        public void SaveToReviewDb(Review review)
        {
            _theDbContext.Reviews.Add(review);
        }


    }
}
