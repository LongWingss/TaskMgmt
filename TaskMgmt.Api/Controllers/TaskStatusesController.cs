using Microsoft.AspNetCore.Mvc;
using TaskMgmt.DataAccess.Repositories;
using TaskMgmt.Services.DTOs;
using TaskMgmt.Services.ProjectTasks;


namespace TaskMgmt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskStatusesController : ControllerBase
    {

        private readonly IProjectTaskStatusService _projectTaskStatusService;
        public TaskStatusesController(IProjectTaskStatusService projectTaskStatusService)
        {
            this._projectTaskStatusService = projectTaskStatusService;
        }

        // GET: api/<TaskStatusesController>
        [HttpGet]
        public IActionResult GetAll(int projectId)
        {
            var Statuses = _projectTaskStatusService.GetAll(projectId);
            if (Statuses == null)
            {
                return NotFound();
            }
            return Ok(Statuses);
        }

        // GET api/<TaskStatusesController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var Status = _projectTaskStatusService.GetById(id);
            if(Status == null)
            {
                return NotFound();

            }
            return Ok(Status);
        }

        // POST api/<TaskStatusesController>
        [HttpPost]
        public IActionResult Post([FromBody] ProjectTaskStatusCreateDto value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            _projectTaskStatusService.Add(value);

            
            return CreatedAtAction(nameof(Post), value);


        }

        // DELETE api/<TaskStatusesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
