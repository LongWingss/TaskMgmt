using Microsoft.AspNetCore.Mvc;
using TaskMgmt.Services.DTOs;
using TaskMgmt.Services.ProjectTasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskMgmt.Api.Controllers
{
    [Route("/projects/{projectId}/tasks")]
    [ApiController]
    public class ProjectTaskController : ControllerBase
    {

        private readonly IProjectTaskService _projectTaskService;

        public ProjectTaskController(IProjectTaskService projectTaskService)
        {
            _projectTaskService = projectTaskService;
        }

        // GET: api/{ProjectId}/
        [HttpGet]
        public async Task<IActionResult> GetAll(int ProjectId)
        {

            var values = await _projectTaskService.GetAll(ProjectId);
            if (!values.Any())
            {
                return NotFound();
            }
            return Ok(values);
        }

        // GET api/<TasksController>/5
        [HttpGet("{taskId}")]
        public async Task<IActionResult> GetById(int taskId)
        {
            var value = await _projectTaskService.Get(taskId);
            if (value == null)
            {
                return NotFound();
            }
            return Ok(value);
        }

        // POST api/<TasksController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NewTaskDto obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _projectTaskService.CreateTask(obj);
            return CreatedAtAction(nameof(GetById),new { id = obj.ProjectId }, obj); // TODO: FIX No route
        }
    }
}
