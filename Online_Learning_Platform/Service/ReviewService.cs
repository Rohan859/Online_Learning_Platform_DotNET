using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Online_Learning_Platform.AllDbContext;
using Online_Learning_Platform.DTOs.ResuestDTO;
using Online_Learning_Platform.Interfaces;
using Online_Learning_Platform.Model;
using Online_Learning_Platform.RepositoryInterface;

namespace Online_Learning_Platform.Service
{
    public class ReviewService : IReviewService
    {
       
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(
           
            IMapper mapper,
            IUserRepository userRepository,
            ICourseRepository courseRepository,
            IReviewRepository reviewRepository
            )
        {
            
            _mapper = mapper;
            _userRepository = userRepository;
            _courseRepository = courseRepository;
            _reviewRepository = reviewRepository;
        }

        public string SubmitReview(ReviewRequestDTO reviewRequestDTO)
        {
            //1. find the user and course
            //2. validate them
            var user = _userRepository.FindUserById(reviewRequestDTO.UserId);

            if (user == null)
            {
                throw new Exception("User Not Found");
            }

            var course = _courseRepository.FindCourseById(reviewRequestDTO.CourseId);

            if (course == null)
            {
                throw new Exception("Course Not Found");
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
           _reviewRepository.SaveToReviewDb(review);
            //_theDbContext.Courses.Update(course);
            //_theDbContext.Users.Update(user);
            _reviewRepository.Save();

            return $"Review is successfully submitted by {user.UserName} for the {course.CourseName} course and review id is {review.ReviewId}";

        }


        public string UpdateReview(Guid reviewId,string description)
        {
            var review = _reviewRepository.FindReviewById(reviewId);

            if(review == null)
            {
                throw new Exception("Review is not exist in our system");
            }

            review.Description = description;

            //_theDbContext.Reviews.Update(review);
            _reviewRepository.UpdateReview(review);
            _reviewRepository.Save();

            return "Review is updated successfully";
        }


        public string DeleteReview(Guid reviewId)
        {
            //first fetch the review
            var review = _reviewRepository
                .FindReviewByIdAndIncludeUserAndCourse(reviewId);

            //then validate it
            if (review==null)
            {
                throw new Exception("Review is not exist in our system");
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
            _reviewRepository.DeleteReview(review);
            _reviewRepository.Save();

            return "Review is successfully deleted";
        }

    }
}
