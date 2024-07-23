

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Online_Learning_Platform.AllDbContext;
using Online_Learning_Platform.Model;
using Online_Learning_Platform.RepositoryInterface;

namespace Online_Learning_Platform.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly AllTheDbContext _theDbContext;
        private readonly IMemoryCache _cache;

        public ReviewRepository(AllTheDbContext allTheDbContext,
            IMemoryCache cache)
        {
            _theDbContext = allTheDbContext;
            _cache = cache;
        }

        public void DeleteReview(Review review)
        {
            _theDbContext.Reviews.Remove(review);
        }

        public Review? FindReviewById(Guid reviewId)
        {
            return _cache.GetOrCreate($"Review_{reviewId}", entry =>
            {
                //cache is valid only for 5 minutes
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);

                var review = _theDbContext.Reviews.Find(reviewId);
                return review;
            });
            //var review = _theDbContext.Reviews.Find(reviewId);
            //return review;
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

        public void UpdateReview(Review review)
        {
            _theDbContext.Reviews.Update(review);
        }
    }
}
