using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Learning_Platform.DTOs.ResponseDTO;
using Online_Learning_Platform.DTOs.ResuestDTO;
using Online_Learning_Platform.Interfaces;
using Online_Learning_Platform.Service;

namespace Online_Learning_Platform.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }


        private ResponseDTO CreateReviewResponse(string message, string result)
        {
            var response = new ResponseDTO
            {
                Message = message,
                Result = result

            };
            return response;
        }


        [HttpPost("/submitReview")]
        public ActionResult<ResponseDTO> SubmitReview([FromBody]ReviewRequestDTO reviewRequestDTO)
        {
            try
            {
                var res = _reviewService.SubmitReview(reviewRequestDTO);

                var response = CreateReviewResponse("Review Submitted", res);

                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new {error = e.Message});
            }

        }

        [HttpPut("/updateReview")]
        public ActionResult<ResponseDTO> UpdateReview(Guid reviewId, string description)
        {
            try
            {
                var res = _reviewService.UpdateReview(reviewId, description);

                var response = CreateReviewResponse("Review Updated", res);

                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new {error = e.Message});
            }
        }

        [HttpDelete("/deleteReview")]
        public ActionResult<string> DeleteReview(Guid reviewId)
        {
            try
            {
                var res = _reviewService?.DeleteReview(reviewId);

                var response = CreateReviewResponse("Review Deleted", res!);

                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message});    
            }
        }
    }
}
