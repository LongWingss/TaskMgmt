using Microsoft.AspNetCore.Mvc;
using TaskMgmt.Services.ProjectTasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskMgmt.Api.Controllers
{
    [Route("/api/{ProjectId}/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {

        private readonly IProjectTaskService _projectTaskService;

        public TasksController(IProjectTaskService projectTaskService)
        {
            _projectTaskService = projectTaskService;
        }


        // GET: api/{ProjectId}/
        [HttpGet]
        public IActionResult Get(int ProjectId)
        {
            return Ok(_projectTaskService.GetAll(ProjectId));
        }

        // GET api/<TasksController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return Ok(_project); ;
        }

        // POST api/<TasksController>
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }

        // PUT api/<TasksController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TasksController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
