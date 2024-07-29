using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Learning_Platform.DTOs.ResponseDTO;
using Online_Learning_Platform.Enums;
using Online_Learning_Platform.Interfaces;
using Online_Learning_Platform.Service;

namespace Online_Learning_Platform.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CourseAnalytics : ControllerBase
    {
        private readonly ICourseAnalyticsService _courseAnalyticsService;


        public CourseAnalytics(ICourseAnalyticsService courseAnalyticsService)
        {
            _courseAnalyticsService = courseAnalyticsService;
        }


        private ResponseDTO CreateCourseAnalyticsResponse(string message, string result)
        {
            var response = new ResponseDTO
            {
                Message = message,
                Result = result

            };
            return response;
        }


        [HttpGet("/getRevenueByCourseId")]
        public ActionResult<ResponseDTO> CalculateTotalRevenueByCourseId([FromQuery]Guid courseId)
        {
           try
            {
                var ans = _courseAnalyticsService.CalculateTotalRevenueByCourseId(courseId);

                var response = CreateCourseAnalyticsResponse
                    ("Revenue calculated for the course",
                    $"Total revenue is {ans}");

                return Ok(response);
            }
            catch(Exception e)
            {
                return NotFound(new {error = e.Message});
            }
            
        }
        
        [HttpGet("/countProgress")]
        public ActionResult<ResponseDTO> CountNoOfOngoingCourses([FromQuery]Progress progress)
        {
            int ans = _courseAnalyticsService.CountNoOfProgress(progress);

            var response = CreateCourseAnalyticsResponse
                ("Counting the no of the course progress",
                 $"Number of {progress} courses are {ans}"
                );
           
            return Ok(response);
        }
    }
}
