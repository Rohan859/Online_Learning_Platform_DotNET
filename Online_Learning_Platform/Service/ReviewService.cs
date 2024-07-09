using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Online_Learning_Platform.AllDbContext;
using Online_Learning_Platform.DTOs;
using Online_Learning_Platform.Interfaces;
using Online_Learning_Platform.Model;

namespace Online_Learning_Platform.Service
{
    public class ReviewService : IReviewService
    {
        private readonly AllTheDbContext _theDbContext;
        private readonly IMapper _mapper;

        public ReviewService(AllTheDbContext allTheDbContext,IMapper mapper)
        {
            _theDbContext = allTheDbContext;
            _mapper = mapper;
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

            //Review review = new Review
            //{
            //    ReviewId = Guid.NewGuid(),
            //    Description = reviewRequestDTO.Description,
            //    User = user,
            //    Course = course,

            //};

            var review = _mapper.Map<Review>(reviewRequestDTO);
            review.ReviewId = Guid.NewGuid();

            user.Reviews.Add(review);
            course.Reviews.Add(review);

            //4. save the review in db
            _theDbContext.Reviews.Add(review);
            //_theDbContext.Courses.Update(course);
            //_theDbContext.Users.Update(user);
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


        public string DeleteReview(Guid reviewId)
        {
            //first fetch the review
            var review = _theDbContext.Reviews
                .Include(e => e.User)
                .Include(e => e.Course)
                .FirstOrDefault(e =>  e.ReviewId == reviewId);

            //then validate it
            if(review==null)
            {
                return "Review is not exist in our system";
            }

            //find the user and course
            var user = review.User;
            var course = review.Course;

            //remove the review from the list of reviews 
            //course and user entity
            user?.Reviews.Remove(review);
            course?.Reviews.Remove(review);

            //in the review entity
            //set null in user and course
            review.Course = null;
            review.User = null;

            //delete this review
            _theDbContext.Reviews.Remove(review);
            _theDbContext.SaveChanges();

            return "Review is successfully deleted";
        }

    }
}
