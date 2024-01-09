using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMgmt.DataAccess.Models;
using TaskMgmt.DataAccess.Repositories;
using TaskMgmt.Services.DTOs;

namespace TaskMgmt.Services.ProjectTasks
{
    public class ProjectTaskStatusService : IProjectTaskStatusService
    {
        private readonly IProjectTaskStatusRepository _projectTaskStatusRepository;

        public ProjectTaskStatusService(IProjectTaskStatusRepository projectTaskStatusRepository)
        {
            _projectTaskStatusRepository = projectTaskStatusRepository;
        }


        public async Task Add(ProjectTaskStatusDto newStatus)
        {
            var status = new ProjectTaskStatus
            {

            };
        }

        public async Task<IEnumerable<ProjectTaskStatusDto>> GetAll(int projectId)
        {
            var allStatuses = await _projectTaskStatusRepository.GetAll();
            var projectStatus = allStatuses.ToList().Where(i => i.ProjectId == projectId);
            var results = new List<ProjectTaskStatusDto>();
            foreach (var status in projectStatus) {
                results.Add(new ProjectTaskStatusDto
                {
                    ProjectTaskStatusId = status.ProjectTaskStatusId,
                    ProjectId = status.ProjectId,
                    StatusText = status.StatusText,
                    StatusColor = status.StatusColor,
                });
            }

            return results;

        }

        public Task<ProjectTaskStatusDto> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
