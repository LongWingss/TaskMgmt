using Microsoft.AspNetCore.Mvc;
using TaskMgmt.Services.DTOs;
using TaskMgmt.Services.ProjectTasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskMgmt.Api.Controllers
{
    [Route("/api/{ProjectId}/[controller]")]
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

            var value = await _projectTaskService.GetAll(ProjectId);
            if (value == null)
            {
                return NotFound();
            }

            return Ok(value);

        }

        // GET api/<TasksController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            var values = await _projectTaskService.Get(id);
            if (values == null)
            {
                return NotFound();
            }
            return Ok(values);


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

        // PUT api/<TasksController>/5
        //[HttpPut("{id}")]
        //public IActionResult Put(int id, [FromBody] string value)
        //{
        //    if (!ModelState.IsValid) { return  BadRequest(ModelState); }

        //    _projectTaskService.

        //}

        //// DELETE api/<TasksController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
