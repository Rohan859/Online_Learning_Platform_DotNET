using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Online_Learning_Platform.DTOs.ResponseDTO;
using Online_Learning_Platform.Enums;
using Online_Learning_Platform.Interfaces;
using Online_Learning_Platform.Model;
using Online_Learning_Platform.Service;

namespace Online_Learning_Platform.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        //private EnrollmentService _enrollmentService;
        private readonly IEnrollmentService _enrollmentService;
        public EnrollmentController(IEnrollmentService service)
        {
            _enrollmentService = service;
        }

        private ResponseDTO CreateEnrollmentResponse(string message, string result)
        {
            var response = new ResponseDTO
            {
                Message = message,
                Result = result

            };
            return response;
        }


        [HttpPost("/enroll")]
        //[Authorize(Roles = "User")]
        public ActionResult<ResponseDTO> EnrollInACourse(
            [FromQuery]Guid userId, 
            [FromQuery]Guid courseId,
            [FromQuery] string countryName)
        {
            try
            {
                var res = _enrollmentService.EnrollInACourse(userId, courseId,countryName);

                var response = CreateEnrollmentResponse("Enrollment Successful", res);

                return Ok(response);
                //return Ok(res);
            }
            catch (Exception e)
            {
                return NotFound(new { error = e.Message });
            }
        }

        [HttpDelete("/unenroll")]
       // [Authorize(Roles = "Admin")]
        public ActionResult<string> UnEnroll([FromQuery]Guid enrollmentId)
        {
            try
            {
                var res = _enrollmentService.UnEnroll(enrollmentId);

                var response = CreateEnrollmentResponse("Unenrollment Successful", res);

                return Ok(response);
            }
            catch (Exception e)
            {
                return NotFound(new { error = e.Message });
            }
        }


        [HttpGet("/trackProgress")]
        public ActionResult<List<Enrollment>> TrackProgress(Progress progress)
        {
            List<Enrollment>enrollments = _enrollmentService.TrackProgress(progress);
            return Ok(enrollments);
        }

    }
}
