using Microsoft.AspNetCore.Http;
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
    }
}
