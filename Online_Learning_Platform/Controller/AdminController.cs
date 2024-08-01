using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Learning_Platform.DTOs.ResponseDTO;
using Online_Learning_Platform.DTOs.ResuestDTO;
using Online_Learning_Platform.Model;
using Online_Learning_Platform.Service;
using Online_Learning_Platform.ServiceInterfaces;
using System.Security.Claims;

namespace Online_Learning_Platform.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IJwtService _jwtService;
        public AdminController(IAdminService adminService,
            IJwtService jwtService)
        {
            _adminService = adminService;
            _jwtService = jwtService;
        }

        [HttpPost("/signUpAdmin")]
        public IActionResult AdminRegister([FromBody]AdminRegisterRequestDTO adminRegisterRequestDTO)
        {
            string res = _adminService.RegisterAdmin(adminRegisterRequestDTO);

            //make claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, adminRegisterRequestDTO.AdminEmail!),
                new Claim(ClaimTypes.Role,"Admin")

            };

            //generate token
            var token = _jwtService.GenerateToken(claims);
            return Ok(new {success = $"{res} and your token is {token}"});
        }

        
        [HttpPost("/adminLogin")]
        public IActionResult AdminLogin([FromBody] LoginRequestDTO loginRequestDTO)
        {
            Admin admin = _adminService.FindAdminByEmail(loginRequestDTO.Email)!;

            if (admin == null)
            {
                return Unauthorized("Wrong credential");
            }

            if (admin.AdminPassword != loginRequestDTO.Password)
            {
                return Unauthorized("Wrong credential");
            }

            //make claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, loginRequestDTO.Email),
                new Claim(ClaimTypes.Role,"Admin")

            };

            //generate token
            var token = _jwtService.GenerateToken(claims);

            return Ok(new { token = token });
        }

        [HttpDelete("/deleteAdminById")]
       // [Authorize(Roles = "Admin")]
        public ActionResult<ResponseDTO> DeleteAdminById([FromQuery]Guid adminId)
        {
            try
            {
                string res = _adminService.DeleteAdminById(adminId);

                var response = new ResponseDTO
                {
                    Message = "Delete Successful",
                    Result = res
                };

                return Ok(response);
            }
            catch (Exception e)
            {
                var response = new ResponseDTO
                {
                    Message = "Error",
                    Result = e.Message
                };
                return NotFound(response);
            }
        }

    }
}
