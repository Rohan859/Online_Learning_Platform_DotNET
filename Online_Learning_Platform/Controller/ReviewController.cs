using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Learning_Platform.DTOs;
using Online_Learning_Platform.Service;

namespace Online_Learning_Platform.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private ReviewService _reviewService;

        public ReviewController(ReviewService reviewService)
        {
            _reviewService = reviewService;
        }


        [HttpPost("/submitReview")]
        public ActionResult<string> SubmitReview([FromBody]ReviewRequestDTO reviewRequestDTO)
        {
            var res = _reviewService.SubmitReview(reviewRequestDTO);

            if(res == "User Not Found" || res == "Course Not Found")
            {
                return NotFound(res);
            }

            return Ok(res);
        }

        [HttpPut("/updateReview")]
        public ActionResult<string> UpdateReview(Guid reviewId, string description)
        {
            var res  = _reviewService.UpdateReview(reviewId,description);

            if(res == "Review is not exist in our system")
            {
                return NotFound(res);
            }

            return Ok(res);
        }
    }
}
