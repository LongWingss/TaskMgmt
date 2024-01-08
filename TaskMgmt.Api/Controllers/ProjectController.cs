using Microsoft.AspNetCore.Mvc;
using TaskMgmt.DataAccess.Models;
using TaskMgmt.DataAccess.Repositories;

namespace TaskMgmt.Api.Controllers
{
    [ApiController]
    [Route("groups/{groupId}/projects")]
    public class ProjectController : ControllerBase
    {

        private readonly IProjectRepository _projectRepository;
        public ProjectController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;

        }

        // GET: groups/{groupId}/projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects(int groupId)
        {
            try
            {
                var projects = await _projectRepository.GetAllAsync(groupId);
                return Ok(projects);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET: /groups/{groupId}/projects/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetById(int groupId, int id)
        {
            try
            {
                var project = await _projectRepository.GetByIdAsync(groupId, id);
                if (project == null)
                {
                    return NotFound();
                }
                return Ok(project);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, ex.Message);
            }
        }

        // POST: /groups/{groupId}/projects
        [HttpPost]
        public async Task<IActionResult> Create(int groupId, [FromBody] Project project)
        {
            try
            {
                await _projectRepository.CreateAsync(groupId, project);
                return CreatedAtAction(nameof(GetById), new { groupId, id = project.ProjectId }, project);
            }
            catch (Exception ex)
            {
                // Log the exception
                return BadRequest(ex.Message);
            }
        }
    }

}
