using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMgmt.DataAccess.Models;
using TaskMgmt.Services.DTOs;

namespace TaskMgmt.Services.ProjectTasks
{
    public class ProjectTaskStatusService : IProjectTaskStatusService
    {
        private readonly IProjectTaskStatusService _projectTaskStatusService;

        public ProjectTaskStatusService(IProjectTaskStatusService projectTaskStatusService)
        {
            _projectTaskStatusService = projectTaskStatusService;
        }


        public async Task Add(ProjectTaskStatusDto newStatus)
        {
            var status = new ProjectTaskStatus
            {

            };
        }

        public Task<IEnumerable<ProjectTaskStatusDto>> GetAll(int projectId)
        {
            var allStatuses = _projectTaskStatusService.GetAll();

        }

        public Task<ProjectTaskStatusDto> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
