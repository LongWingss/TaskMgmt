using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskMgmt.DataAccess.Models;
using TaskMgmt.Services;

namespace TaskMgmt.Api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class InviteController : Controller
    {
        public IGroupService _inviteservices;

        public InviteController(IGroupService inviteservices)
        {
            
            _inviteservices = inviteservices;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("{id}/enroll")]
        [Authorize]
        public async Task<IActionResult> Enroll(Invitation invitation, string referralCode)
        {
            int id = int.Parse(User.FindFirstValue("userId"));
            var enroll = await _inviteservices.Enroll(invitation, referralCode, id);

            return Ok(enroll);
        }
    }
}
