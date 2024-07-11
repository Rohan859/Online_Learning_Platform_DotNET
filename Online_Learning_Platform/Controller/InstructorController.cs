using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Learning_Platform.DTOs;
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

        [HttpPost("/instructorRegister")]
        public ActionResult<string>Register([FromBody]Instructor instructor)
        {
            string res = _instructorService.Register(instructor);
            return Ok(res);
        }


        [HttpPut("/updateInstructor")]
        public ActionResult<string> updateInstructor([FromBody]InstructorUpdateRequestDTO instructorUpdateRequestDTO)
        {
            string res=_instructorService.updateInstructor(instructorUpdateRequestDTO);

            if(res=="Not Found")
            {
                return NotFound("Not Found");
            }
            return Ok(res);
        }


        [HttpDelete("/deleteInstructor")]
        public ActionResult<string> DeleteInstructor([FromQuery]Guid id)
        {
            string res=_instructorService.RemoveInstructor(id);
            if(res=="Not Found")
            {
                return NotFound("Instructor is not found");
            }

            if(res == "Either course is null or instructors list is empty")
            {
                return BadRequest(res);
            }
            return Ok(res);
        }


        [HttpPost("/assignInstructorByCourseId")]
        public ActionResult<string> AssignInstructor([FromQuery]Guid instructorId,[FromQuery]Guid courseId)
        {
            string res = _instructorService.AssignInstructor(instructorId, courseId);

            if(res== "Instructor not found")
            {
                return NotFound(res);
            }

            if (res == "Course not found")
            {
                return NotFound(res);
            }

            return Ok(res);
        }


        [HttpGet("/noOfInstructorsByCourseId")]
        public ActionResult<string>GetCountOfInstructorByCourseId([FromQuery]Guid courseId)
        {
            try
            {
                var ans = _instructorService.GetCountOfInstructorByCourseId(courseId);
                return Ok($"No of instructor for the course is {ans}");
            }
            catch (Exception e)
            {
                Console.WriteLine("there is some error - "+e.Message);
            }

            return BadRequest("Course is not exist");
        }

        [HttpGet("/getListOfInstructorsByCourseId")]
        public ActionResult<List<Instructor>> GetListOfInstructorsByCourseId([FromQuery]Guid courseId)
        {
            List<Instructor>instructors = _instructorService
                .GetListOfInstructorsByCourseId(courseId) ;

            if(instructors.Count==0)
            {
                return NotFound("No instructor found");
            }

            return Ok(instructors);
        }

        
    }
}
