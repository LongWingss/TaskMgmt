using Microsoft.AspNetCore.Mvc;
using TaskMgmt.Api.Dtos.User;
using TaskMgmt.DataAccess.Repositories;
using TaskMgmt.Services;

namespace TaskMgmt.Api.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : Controller
    {
        public IUserRepository _userRepository;
        public LoginController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            var authenticationResult = _userRepository
            return View();
        }
    }
}
