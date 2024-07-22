using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Learning_Platform.AllDbContext;
using Online_Learning_Platform.DTOs.ResponseDTO;
using Online_Learning_Platform.DTOs.ResuestDTO;

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

        private ResponseDTO CreateUserResponse(string message,string result)
        {
            var response = new ResponseDTO
            {
                Message = message,
                Result = result

            };
            return response;
        }
        

        [HttpPost("/userRegister")]
        public ActionResult<ResponseDTO> Register([FromBody]UserRegistrationRequestDTO userRegistrationRequestDTO)
        {
            try
            {
                string res = _userService.Register(userRegistrationRequestDTO);

                var response = CreateUserResponse("Registration Successful",res);
                
                return Ok(response);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(new { error = e.Message });
            }
        }



        [HttpDelete("/deleteUser")]
        public ActionResult<ResponseDTO> DeleteUserById([FromQuery] Guid userId) 
        {
            try
            {
                string res = _userService.DeleteUserById(userId);

                var response = CreateUserResponse("Deleted Successfully", res);
                
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new {error = e.Message});
            }

            
        }

        [HttpPut("/updateUser")]
        public ActionResult<ResponseDTO> UpdateUserProfile([FromBody]UserProfileUpdateRequestDTO userProfileUpdateRequestDTO)
        {
            try
            {
                string res = _userService.UpdateUserProfile(userProfileUpdateRequestDTO);

                var response = CreateUserResponse("Updated Successfully", res);

                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }

           
        }


        [HttpGet("/getCourseListByUser")]
        public ActionResult<List<Course>> GetCourseListForUserById([FromQuery]Guid userId)
        {
            try
            {
                var res = _userService.GetCourseListForUserById(userId);

                var response = new CourseListResponseDTO
                {
                    Message = "Successfully got the courses",
                    Courses = res
                };

                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }

        }

        [HttpGet("/countEnrollCoursesByUserId")]
        public ActionResult<ResponseDTO> CountEnrollCoursesByUserId([FromQuery]Guid userId)
        {
            try
            {
                var res = _userService.CountEnrollCoursesByUserId(userId);

                var response = CreateUserResponse("Getting number of enrollments",
                    $"No of enrollments are {res}");

                return Ok(response);

            }
            catch (Exception e)
            {
                return BadRequest(new {error = e.Message});
            }

            
        }

        [HttpGet("/getNoOfReviewsByUserId")]
        public ActionResult<ResponseDTO> GetNoOfReviewsByUserId([FromQuery] Guid userId)
        {
            try
            {
                int noOfReviews = _userService.GetNoOfReviewsByUserId(userId);

                var response = CreateUserResponse("Getting number of Reviews",
                    $"No of reviews are {noOfReviews}");

                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new {error = e.Message});
            }

           
        }

        
    }
}
