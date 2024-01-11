using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskMgmt.Api.Attributes;
using TaskMgmt.Services.CustomExceptions;
using TaskMgmt.Services.DTOs;
using TaskMgmt.Services.ProjectTasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskMgmt.Api.Controllers
{
    [Route("api/groups/{groupId}/projects/{projectId}/tasks")]
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
        [GroupMembershipAuthorize("groupId")]
        [Authorize]
        public async Task<IActionResult> GetAll(int groupId, int projectId)
        {
            var values = await _projectTaskService.GetAll(groupId, projectId);
            return Ok(values);
        }

        // GET api/<TasksController>/5
        [HttpGet("{taskId}")]
        [GroupMembershipAuthorize("groupId")]
        [Authorize]
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
        [GroupMembershipAuthorize("groupId")]
        [Authorize]
        public async Task<IActionResult> CreateTask(int groupId, int projectId, [FromBody] NewTaskDto obj)
        {
            try
            {
                var userId = Convert.ToInt32(HttpContext.User.FindFirst("UserId")?.Value);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                ProjectTaskDto newTask;
                try
                {
                    newTask = await _projectTaskService.CreateTask(userId, groupId, projectId, obj);
                }
                catch (AssigneeNotFoundException e)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, e.Message);
                }
                catch (ProjectNotFoundException e)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, e.Message);
                }

                return CreatedAtAction(nameof(GetById), new { projectId, groupId, taskId = newTask.TaskId}, newTask);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
