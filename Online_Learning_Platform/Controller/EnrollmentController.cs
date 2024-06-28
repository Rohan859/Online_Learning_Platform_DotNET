using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Online_Learning_Platform.Service;

namespace Online_Learning_Platform.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private EnrollmentService _enrollmentService;

        public EnrollmentController(EnrollmentService service)
        {
            _enrollmentService = service;
        }


        [HttpPost("/enroll")]
        public ActionResult<string> EnrollInACourse([FromQuery]Guid userId, [FromQuery]Guid courseId)
        {
            try
            {
                string res = _enrollmentService.EnrollInACourse(userId, courseId);
                return Ok(res);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error happened here! {e.Message}");
            }
            return BadRequest("Either your course is not exist or user is not exist in our system");
        }


        [HttpDelete("/deleteEnrollment")]
        public ActionResult<string> DeleteEnrollment([FromQuery]Guid enrollmentId)
        {
            var res = _enrollmentService.DeleteEnrollment(enrollmentId);

            if(res == "Not Found")
            {
                return NotFound(res);
            }
            return Ok(res);
        }
    }
}
