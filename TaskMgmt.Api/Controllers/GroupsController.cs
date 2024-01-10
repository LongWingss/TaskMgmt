using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.RegularExpressions;
using TaskMgmt.Api.Attributes;
using TaskMgmt.Api.DTO;
using TaskMgmt.Api.DTO.User;
using TaskMgmt.DataAccess.Models;
using TaskMgmt.DataAccess.Repositories;
using TaskMgmt.Services;
using TaskMgmt.Services.CustomExceptions;
using Group = TaskMgmt.DataAccess.Models.Group;

namespace TaskMgmt.Api.Controllers
{
    [Route("api/groups")]
    [ApiController]
    public class GroupsController : Controller
    {
        private readonly IGroupService _groupService;
        public GroupsController(IGroupService groupService, IUserService userService)
        {
            _groupService = groupService;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                if (int.TryParse(User.FindFirst("UserId").Value, out int userid))
                {
                    var groups = await _groupService.GetAll(userid);
                    return Ok(groups);
                }
                else
                {
                    return BadRequest("Invalid UserId");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        // [Authorize]
        // [HttpGet("{id}")]
        // public async Task<IActionResult> GetById(int id)
        // {
        //     try
        //     {
        //         var group = await _groupService.GetById(id);
        //         return Ok(group);
        //     }
        //     catch (GroupNotFoundException ex)
        //     {
        //         return BadRequest(ex.Message);
        //     }
        // }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] GroupDTO groupDto)
        {
            try
            {
                if (groupDto == null)
                    return BadRequest();

                var group = new Group { GroupName = groupDto.GroupName };
                var userId = Convert.ToInt32(User.FindFirst("UserId").Value);
                var createdGroup = await _groupService.Add(group, userId);
                return Created("Created", createdGroup);

            }
            catch (GroupAlreadyExistsException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [GroupMembershipAuthorize("groupId")]
        [HttpPost("{groupId}/invitations")]
        public async Task<IActionResult> InviteUser(int groupId, [FromBody] EmailDTO email)
        {
            try
            {
                int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
                var invitationId = await _groupService.InviteUser(userId, groupId, email.Email);
                return Ok(invitationId);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("enrollments")]
        [Authorize]
        public async Task<IActionResult> Enroll([FromBody] InvitationDTO invitation)
        {
            try
            {
                int userId = int.Parse(User.FindFirstValue("UserId"));
                await _groupService.Enroll(userId, invitation.GroupName, invitation.ReferralCode);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
