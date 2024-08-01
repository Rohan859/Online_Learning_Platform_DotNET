using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Learning_Platform.DTOs.ResponseDTO;
using Online_Learning_Platform.DTOs.ResuestDTO;
using Online_Learning_Platform.Interfaces;
using Online_Learning_Platform.Model;
using Online_Learning_Platform.Service;
using Online_Learning_Platform.ServiceInterfaces;
using System.Security.Claims;

namespace Online_Learning_Platform.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        //private readonly InstructorService _instructorService;
        private readonly IInstructorService _instructorService;
        private readonly IJwtService _jwtService;
        public InstructorController(IInstructorService instructorService,
            IJwtService jwtService)
        {
            _instructorService = instructorService;
            _jwtService = jwtService;
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

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, instructor.Email!),
                    new Claim(ClaimTypes.Role,"Tutor")

                };

                //generate token
                var token = _jwtService.GenerateToken(claims);

                var response = CreateInstructorResponse($"Successfully Registered the instructor and the token is {token}", res);

                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new {error = e.Message});
            }
            
        }


        [HttpPut("/updateInstructor")]
       // [Authorize(Roles = "Tutor")]
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
      //  [Authorize(Roles = "Admin")]
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
       // [Authorize(Roles = "Admin")]
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
       // [Authorize(Roles = "Admin")]
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
      //  [Authorize(Roles = "Admin")]
        public ActionResult<InstructorListResponseDTO> GetListOfInstructorsByCourseId([FromQuery] Guid courseId)
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

        [HttpPost("/instructorLogin")]
        public IActionResult InstructorLogin([FromBody] LoginRequestDTO loginRequestDTO)
        {
            Instructor instructor = _instructorService
                .FindInstructorByEmail(loginRequestDTO.Email)!;

            if (instructor == null)
            {
                return Unauthorized("Wrong credential");
            }

            if (instructor.Password != loginRequestDTO.Password)
            {
                return Unauthorized("Wrong credential");
            }

            //make claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, loginRequestDTO.Email),
                new Claim(ClaimTypes.Role,"Tutor")

            };

            //generate token
            var token = _jwtService.GenerateToken(claims);

            return Ok(new { token = token });
        }
    }
}
