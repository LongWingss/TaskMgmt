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


        public async Task Add(ProjectTaskStatusCreateDto newStatus)
        {

            var status = new ProjectTaskStatus
            {
                ProjectId=newStatus.ProjectId,
                StatusText=newStatus.StatusText,
                StatusColor=newStatus.StatusColor
            };
            await _projectTaskStatusRepository.Add(status);

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

        public async Task<ProjectTaskStatusDto> GetById(int id)
        {
            var task=await _projectTaskStatusRepository.GetById(id);
            var obj = new ProjectTaskStatusDto{
                ProjectTaskStatusId = task.ProjectTaskStatusId,
                ProjectId = task.ProjectId,
                StatusText = task.StatusText,
                StatusColor= task.StatusColor
            };
            return obj;
        }
    }
}
