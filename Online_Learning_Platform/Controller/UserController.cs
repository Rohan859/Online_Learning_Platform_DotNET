using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Learning_Platform.AllDbContext;
using Online_Learning_Platform.DTOs.ResponseDTO;
using Online_Learning_Platform.DTOs.ResuestDTO;
using Online_Learning_Platform.Filter;
using Online_Learning_Platform.Interfaces;
using Online_Learning_Platform.Model;
using Online_Learning_Platform.Security;
using Online_Learning_Platform.Service;
using Online_Learning_Platform.ServiceInterfaces;
using System.Security.Claims;
using UuidExtensions;

namespace Online_Learning_Platform.Controller
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //private readonly UserService _userService;
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;
        public UserController(IUserService userService,
            IJwtService jwtService)
        {
           _userService = userService;
           _jwtService = jwtService;
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
                
                //make claims
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, userRegistrationRequestDTO.Email!),
                    new Claim(ClaimTypes.Role,"User")

                };

                //generate token
                var token = _jwtService.GenerateToken(claims);

                var response = CreateUserResponse("Registration Successful",res+$" and the token is {token}");
                
                return Ok(response);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(new { error = e.Message });
            }
        }



        [HttpDelete("/deleteUser")]
        //[Authorize(Roles = "Admin")]
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
        //[Authorize(Roles = "User")]
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
       // [Authorize(Roles = "Admin,User")]
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
       // [Authorize(Roles = "User")]
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
       // [Authorize(Roles = "User")]
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


        [HttpGet("/getUserById")]
        //[Authorize(Roles = "Admin")]
        public ActionResult<User> FindUserById([FromQuery]Guid userId)
        {
            try
            {
                User user = _userService.FindUserById(userId);
                return Ok(user);
            }
            catch(Exception e) 
            {
                return BadRequest(new { error = e.Message });
            }
        }


        [HttpPost("/userLogin")]
        //[AllowAnonymous]
        public IActionResult Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            User user = _userService.IsUserExistByEmail(loginRequestDTO.Email)!;

            if(user == null)
            {
                return Unauthorized("Wrong credential");
            }

            if (user.Password != loginRequestDTO.Password)
            {
                return Unauthorized("Wrong credential");
            }

            //make claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, loginRequestDTO.Email),
                new Claim(ClaimTypes.Role,"User")
               
            };

            //generate token
            var token = _jwtService.GenerateToken(claims);

            return Ok(new {token = token });
        }
    }
}
