using Microsoft.AspNetCore.Mvc;
using TaskMgmt.DataAccess.Repositories;
using TaskMgmt.Services.DTOs;
using TaskMgmt.Services.ProjectTasks;


namespace TaskMgmt.Api.Controllers
{
    [Route("api/groups/{groupId}/projects/{projectId}/statuses")]
    [ApiController]
    public class ProjectTaskStatusController : ControllerBase
    {

        private readonly IProjectTaskStatusService _projectTaskStatusService;
        public ProjectTaskStatusController(IProjectTaskStatusService projectTaskStatusService)
        {
            this._projectTaskStatusService = projectTaskStatusService;
        }

        // GET: api/<TaskStatusesController>
        [HttpGet]
        public async Task<IActionResult> GetAll(int projectId)
        {
            var statuses = await _projectTaskStatusService.GetAll(projectId);
            if (!statuses.Any())
            {
                return NotFound();
            }
            return Ok(statuses);
        }

        // GET api/<TaskStatusesController>/5
        [HttpGet("{statusId}")]
        public IActionResult GetById(int statusId)
        {
            var status = _projectTaskStatusService.GetById(statusId);
            if(status == null)
            {
                return NotFound();
            }
            return Ok(status);
        }

        // POST api/<TaskStatusesController>
        [HttpPost]
        public IActionResult Post([FromRoute] int projectId, [FromBody] ProjectTaskStatusCreateDto value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }
            _projectTaskStatusService.Add(projectId, value);            
            return CreatedAtAction(nameof(Post), value);
        }
    }
}
