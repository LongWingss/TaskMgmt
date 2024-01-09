using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
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
        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
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
                var createdGroup = await _groupService.Add(group);
                return Created("Created", createdGroup);
            }
            catch (GroupAlreadyExistsException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
