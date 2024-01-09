using Microsoft.AspNetCore.Mvc;
using TaskMgmt.Api.Dtos.User;
using TaskMgmt.DataAccess.Repositories;
using TaskMgmt.Services;

namespace TaskMgmt.Api.Controllers
{
    [ApiController]
    public class LoginController : Controller
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("api/login")]
        [HttpPost()]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            try
            {
                string token = await _userService.Authenticate(loginDto.Email, loginDto.Password);
                return Ok(token);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Login failed");
            }

        }
        [Route("api/signup")]
        [HttpPost()]
        public async Task<IActionResult> SignUP([FromBody] LoginDTO loginDto)
        {
            try
            {
                string token = await _userService.Authenticate(loginDto.Email, loginDto.Password);
                return Ok(token);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Login failed");
            }

        }
    }
}
