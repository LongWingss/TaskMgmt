using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using TaskMgmt.DataAccess.Models;
using TaskMgmt.DataAccess.Repositories;
using Group = TaskMgmt.DataAccess.Models.Group;

namespace TaskMgmt.Api.Controllers
{
    [Route("api/groups")]
    [ApiController]
    public class GroupsController : Controller
    {
        private readonly IGroupRepository _groupRepository;
        public GroupsController(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var groups = _groupRepository.GetAll();
                return Ok(groups.Result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var group = _groupRepository.GetById(id);
                if (group == null)
                {
                    return NotFound();
                }
                return Ok(group.Result);
            }
            catch (Exception ex)
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

                var createdGroup = await _groupRepository.Add(group);
                return Created("Created", createdGroup);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  
            }
        }
    }
}
