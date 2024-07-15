using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Learning_Platform.DTOs.ResponseDTO;
using Online_Learning_Platform.DTOs.ResuestDTO;
using Online_Learning_Platform.Interfaces;
using Online_Learning_Platform.Model;
using Online_Learning_Platform.Service;

namespace Online_Learning_Platform.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        //private readonly InstructorService _instructorService;
        private readonly IInstructorService _instructorService;

        public InstructorController(IInstructorService instructorService)
        {
            _instructorService = instructorService;
        }

        private ResponseDTO CreateInstructorResponse(string message, string result)
        {
            var response = new ResponseDTO
            {
                Message = message,
                Result = result

            };
            return response;
        }

        [HttpPost("/instructorRegister")]
        public ActionResult<ResponseDTO> Register([FromBody]Instructor instructor)
        {
            try
            {
                string res = _instructorService.Register(instructor);

                var response = CreateInstructorResponse("Successfully Registered", res);

                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new {error = e.Message});
            }
            
        }


        [HttpPut("/updateInstructor")]
        public ActionResult<ResponseDTO> updateInstructor([FromBody]InstructorUpdateRequestDTO instructorUpdateRequestDTO)
        {
            try
            {
                string res = _instructorService.updateInstructor(instructorUpdateRequestDTO);

                var response = CreateInstructorResponse("Successfully Updated", res);

                return Ok(response);
            }
            catch (Exception e)
            {
                return NotFound(new {error = e.Message});
            }
        }


        [HttpDelete("/deleteInstructor")]
        public ActionResult<ResponseDTO> DeleteInstructor([FromQuery]Guid id)
        {
            try
            {
                string res = _instructorService.RemoveInstructor(id);

                var response = CreateInstructorResponse("Successfully removed the instructor", res);

                return Ok(response);
            }
            catch (Exception e)
            {
                return NotFound(new {error = e.Message});

            }
        }


        [HttpPost("/assignInstructorByCourseId")]
        public ActionResult<ResponseDTO> AssignInstructor([FromQuery]Guid instructorId,[FromQuery]Guid courseId)
        {
            try
            {
                string res = _instructorService.AssignInstructor(instructorId, courseId);

                var response = CreateInstructorResponse("Successfully assigned the instructor for the course", res);

                return Ok(response);
            }
            catch (Exception e)
            {
                return NotFound(new {error = e.Message});
            }
        }


        [HttpGet("/noOfInstructorsByCourseId")]
        public ActionResult<ResponseDTO> GetCountOfInstructorByCourseId([FromQuery]Guid courseId)
        {
            try
            {
                var ans = _instructorService.GetCountOfInstructorByCourseId(courseId);

                var response = CreateInstructorResponse
                    ("Getting no of instructors for the course", 
                    $"No of instructor for the course is {ans}");

                return Ok(response);               
            }
            catch (Exception e)
            {
                return NotFound (new {error = e.Message});
            }           
        }

        [HttpGet("/getListOfInstructorsByCourseId")]
        public ActionResult<List<Instructor>> GetListOfInstructorsByCourseId([FromQuery] Guid courseId)
        {
            try
            {
                List<Instructor> instructors = _instructorService
               .GetListOfInstructorsByCourseId(courseId);

                var response = new InstructorListResponseDTO
                {
                    Message = "Getting available list of instructor for the course",
                    Instructors = instructors
                };

                return Ok(response);
            }
            catch (Exception e)
            {
                return NotFound(new {error = e.Message});
            }
        }

        
    }
}
