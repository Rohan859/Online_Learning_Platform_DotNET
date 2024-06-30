using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Learning_Platform.Service;

namespace Online_Learning_Platform.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseAnalytics : ControllerBase
    {
        private  CourseAnalyticsService _courseAnalyticsService;

        public CourseAnalytics(CourseAnalyticsService courseAnalyticsService)
        {
            _courseAnalyticsService = courseAnalyticsService;
        }

        [HttpGet("/getRevenueByCourseId")]
        public ActionResult<string> CalculateTotalRevenueByCourseId([FromQuery]Guid courseId)
        {
           try
            {
                var ans = _courseAnalyticsService.CalculateTotalRevenueByCourseId(courseId);
                return Ok($"Total revenue is {ans}");
            }
            catch(Exception e)
            {
                Console.WriteLine("there is some issue - "+e.Message);
            }
            return BadRequest("some error is there");
        }
    }
}
