using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Learning_Platform.AllDbContext;
using Online_Learning_Platform.DTOs;
using Online_Learning_Platform.Interfaces;
using Online_Learning_Platform.Model;
using Online_Learning_Platform.Service;
using UuidExtensions;

namespace Online_Learning_Platform.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //private readonly UserService _userService;
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
           _userService = userService;
        }


        [HttpGet("/")]
        public ActionResult<string> Reply() 
        {
           return Ok("Welcome to Online Learning Platform");
        }
        

        [HttpPost("/userRegister")]
        public ActionResult<string>Register([FromBody]UserRegistrationRequestDTO userRegistrationRequestDTO)
        {
            string error = "";
            try
            {
                string res = _userService.Register(userRegistrationRequestDTO);
                return Ok(res);
            }
            catch (Exception e)
            {
                error = e.Message;
                Console.WriteLine(e.Message);
            }
            return BadRequest(error);
        }





        [HttpDelete("/deleteUser")]
        public ActionResult<string> DeleteUserById([FromQuery] Guid userId) 
        {
            string res=_userService.DeleteUserById(userId);

            if(res== "User is not found")
            {
                return NotFound("User is not found");
            }
            return Ok(res);
        }

        [HttpPut("/updateUser")]
        public ActionResult<string>UpdateUserProfile([FromBody]UserProfileUpdateRequestDTO userProfileUpdateRequestDTO)
        {
            string res=_userService.UpdateUserProfile(userProfileUpdateRequestDTO);

            if(res== "Not Found")
            {
                return NotFound("User is not found in our system"); 
            }

            return Ok(res);
        }


        [HttpGet("/getCourseListByUser")]
        public ActionResult<List<Course>> GetCourseListForUserById([FromQuery]Guid userId)
        {
            var res = _userService.GetCourseListForUserById(userId);

            if( res==null)
            {
                return NotFound("User not found in our system");
            }

            return Ok(res);
        }

        [HttpGet("/countEnrollCoursesByUserId")]
        public ActionResult<int> CountEnrollCoursesByUserId([FromQuery]Guid userId)
        {
            var res = _userService.CountEnrollCoursesByUserId(userId);

            return Ok(res);
        }

        [HttpGet("/getNoOfReviewsByUserId")]
        public ActionResult<string> GetNoOfReviewsByUserId([FromQuery] Guid userId)
        {
            try
            {
                int noOfReviews = _userService.GetNoOfReviewsByUserId(userId);
                return Ok($"No of reviews are : {noOfReviews}");
            }
            catch (Exception e)
            {
                Console.WriteLine("the error is "+e.Message);
            }

            return BadRequest("User is not exist");
        }

        
    }
}
