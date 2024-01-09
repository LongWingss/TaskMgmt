using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskMgmt.DataAccess.Dtos;
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> Create(int groupId, [FromBody] ProjectDto projectDto)
        {
            try
            {
                var userId = HttpContext.Items["UserId"] as int?;
                if (userId == null)
                    return Unauthorized();

                var project = new Project
                {
                    ProjectName = projectDto.ProjectName,
                    ProjectDescription = projectDto.ProjectDescription,
                    GroupId = groupId,
                    OwnerId = (int)userId,
                };
                await _projectRepository.CreateAsync(project);
                var responseDto = new ProjectResponseDto
                {
                    ProjectId = project.ProjectId,
                    GroupId = (int)project.GroupId,
                    ProjectDescription = projectDto.ProjectDescription,
                    ProjectName = projectDto.ProjectName,
                    OwnerId = project.OwnerId,

                };
                return CreatedAtAction(nameof(GetById), new { groupId, id = project.ProjectId }, responseDto);
            }
            catch (Exception ex)
            {
                // Log the exception
                return BadRequest(ex.Message);
            }
        }
    }

}
