using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
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
        public IActionResult GetAll()
        {
            
                var groups = _groupService.GetAll();
                return Ok(groups);
           
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var group = _groupService.GetById(id);
                return Ok(group);
            }
            catch (GroupNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Group group)
        {
            try
            {
                if (group == null)
                    return BadRequest();

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
