using Microsoft.AspNetCore.Mvc;
using TaskMgmt.Api.DTO;
using TaskMgmt.DataAccess.Repositories;
using TaskMgmt.Services;
using TaskMgmt.Services.CustomExceptions;

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
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }

        }
        [Route("api/signup")]
        [HttpPost()]
        public async Task<IActionResult> SignUp([FromBody] SignUpDTO signUpDto)
        {
            try
            {
                string token = await _userService.SignUp(signUpDto.Email, signUpDto.Password, signUpDto.Name, signUpDto.GroupName);
                return Ok(token);
            }
            catch (GroupAlreadyExistsException)
            {
                return StatusCode(StatusCodes.Status409Conflict, "Group already exists");
            }
            catch (UserAlreadyExistsException)
            {
                return StatusCode(StatusCodes.Status409Conflict, "User already exists");
            }

        }
    }
}
