using Online_Learning_Platform.AllDbContext;
using Online_Learning_Platform.DTOs;
using Online_Learning_Platform.Model;

namespace Online_Learning_Platform.Service
{
    public class ReviewService
    {
        private readonly AllTheDbContext _theDbContext;

        public ReviewService(AllTheDbContext allTheDbContext)
        {
            _theDbContext = allTheDbContext;
        }

        public string SubmitReview(ReviewRequestDTO reviewRequestDTO)
        {
            //1. find the user and course
            //2. validate them
            var user = _theDbContext.Users.Find(reviewRequestDTO.UserId);
            if (user == null)
            {
                return "User Not Found";
            }

            var course = _theDbContext.Courses.Find(reviewRequestDTO.CourseId);
            if (course == null)
            {
                return "Course Not Found";
            }

            //3. create new review with id 
            // and assign the user and course

            Review review = new Review
            {
                ReviewId = Guid.NewGuid(),
                Description = reviewRequestDTO.Description,
                User = user,
                Course = course,

            };

            user.Reviews.Add(review);
            course.Reviews.Add(review);

            //4. save the review in db
            _theDbContext.Reviews.Add(review);
            _theDbContext.Courses.Update(course);
            _theDbContext.Users.Update(user);
            _theDbContext.SaveChanges();

            return $"Review is successfully submitted by {user.UserName} for the {course.CourseName} course and review id is {review.ReviewId}";

        }


        public string UpdateReview(Guid reviewId,string description)
        {
            var review = _theDbContext.Reviews.Find(reviewId);

            if(review == null)
            {
                return "Review is not exist in our system";
            }

            review.Description = description;

            _theDbContext.Reviews.Update(review);
            _theDbContext.SaveChanges();

            return "Review is updated successfully";
        }

    }
}
