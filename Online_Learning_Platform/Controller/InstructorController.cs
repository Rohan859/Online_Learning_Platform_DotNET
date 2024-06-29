using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Learning_Platform.DTOs;
using Online_Learning_Platform.Model;
using Online_Learning_Platform.Service;

namespace Online_Learning_Platform.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        private readonly InstructorService _instructorService;

        public InstructorController(InstructorService instructorService)
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
    }
}
