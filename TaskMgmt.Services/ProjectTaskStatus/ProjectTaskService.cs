using TaskMgmt.DataAccess.Models;
using TaskMgmt.DataAccess.Repositories;
using TaskMgmt.Services.DTOs;

namespace TaskMgmt.Services.ProjectTaskStatus
{
    public class ProjectTaskService : IProjectTaskService
    {
        private readonly IProjectTaskRepository projectTaskRepository;

        public ProjectTaskService(IProjectTaskRepository repo)
        {
            projectTaskRepository = repo;
        }

        public async Task<ProjectTaskDto?> Get(int taskId)
        {
        
            var currentTask = await projectTaskRepository.GetById(taskId);

            if (currentTask == null) return new ProjectTaskDto();

            var projectTaskDTo = new ProjectTaskDto
            {
                TaskId = currentTask.ProjectTaskId,
                Description = currentTask.Description,
                DueDate = currentTask.DueDate.ToShortTimeString(),
                Assignee = currentTask.Assignee.Email,
                CreatedBy = currentTask.Creator.Email,
                CurrentStatus = currentTask.CurrentStatus.StatusText
            };
            return projectTaskDTo;
        }

        public async Task<IEnumerable<ProjectTaskDto>> GetAll(int projectId)
        {
            var allProjectTasks = await projectTaskRepository.GetAll();
            var currProjectTasks = allProjectTasks.ToList().Where(a => a.ProjectId==projectId);
            var results = new List<ProjectTaskDto>();

            foreach(var task in  currProjectTasks)
            {
                results.Add(new ProjectTaskDto
                {
                    TaskId = task.ProjectTaskId,
                    Description = task.Description,
                    DueDate = task.DueDate.ToString(),
                    Assignee = task.Assignee.Email,
                    CreatedBy = task.Creator.Email,
                    CurrentStatus = task.CurrentStatus.StatusText

                });
            }
            return results;
        }

        public async Task CreateTask(NewTaskDto newTask)
        {
            var task = new ProjectTask
            {
                Description = newTask.Description,
                DueDate = newTask.DueDate,
                AssigneeId = newTask.AssigneeId,
                CreatorId = newTask.CreatorId,
                ProjectId = newTask.ProjectId,
                CurrentStatusId = newTask.CurrentStatusId
            };
            await projectTaskRepository.Add(task);
        }
    }
}
