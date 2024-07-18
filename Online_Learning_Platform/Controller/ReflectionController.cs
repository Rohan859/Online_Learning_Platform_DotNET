using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Learning_Platform.Reflection;

namespace Online_Learning_Platform.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReflectionController : ControllerBase
    {
        private readonly LearningReflection _learningReflection;

        public ReflectionController(LearningReflection learningReflection)
        {
            _learningReflection = learningReflection;
        }

        [HttpGet("/reflect")]
        public ActionResult<string> Reflect([FromQuery]string className,
                                            [FromQuery] string methodName)
        {
            try
            {
                _learningReflection.InvokeMethod(className, methodName);
                return Ok("Success for reflection");
            }
            catch (Exception e)
            {
                return BadRequest(new {error = e.Message });
            }
        }
    }
}
