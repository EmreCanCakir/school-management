using UserManagement.Models;
using UserManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace UserManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDto userLogin)
        {
            if (userLogin.UserName == "test" && userLogin.Password == "test")
            {
                var token = _tokenService.GenerateJwtToken(userLogin.UserName, userLogin.Password);
                return Ok(new { Token = token });
            }
            return Unauthorized("Invalid credentials");
        }
    }
}
