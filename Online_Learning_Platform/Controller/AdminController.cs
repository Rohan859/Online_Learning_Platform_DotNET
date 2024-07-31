using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Learning_Platform.DTOs.ResuestDTO;
using Online_Learning_Platform.Model;
using Online_Learning_Platform.Service;
using Online_Learning_Platform.ServiceInterfaces;
using System.Security.Claims;

namespace Online_Learning_Platform.Controller
{
    [Route("api/[controller]")]
    [ApiController]
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

            return Ok(new {success = res});
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

    }
}
