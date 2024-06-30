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
            var res = _enrollmentService.EnrollInACourse(userId,courseId);

            if(res == "User not found")
            {
                return NotFound(res);
            }

            if (res == "Course not found")
            {
                return NotFound(res);
            }

            return Ok(res);
        }

        [HttpDelete("/unenroll")]
        public ActionResult<string> UnEnroll([FromQuery]Guid enrollmentId)
        {
            var res = _enrollmentService.UnEnroll(enrollmentId);

            if (res == "enrollment not found")
            {
                return NotFound(res);
            }

            return Ok(res);
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
