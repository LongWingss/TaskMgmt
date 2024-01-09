using TaskMgmt.DataAccess.Models;
using TaskMgmt.DataAccess.Repositories;
using TaskMgmt.Services.DTOs;

namespace TaskMgmt.Services.ProjectTasks
{
    public class ProjectTaskService : IProjectTaskService

    {

        private readonly ITaskRepository _taskRepository;

        private readonly IProjectTaskStatusRepository _projectTaskStatusRepository;

        public ProjectTaskService(ITaskRepository taskRepository, IProjectTaskStatusRepository projectTaskStatusRepository)
        {
            _taskRepository = taskRepository;
            _projectTaskStatusRepository = projectTaskStatusRepository;
        }

        public IEnumerable<DataAccess.DTOs.ProjectTask> GetAll(int projectId)
        {
            throw new NotImplementedException();
        }

        public DataAccess.DTOs.ProjectTask Get(int taskId)
        {
            throw new NotImplementedException();
        }

        public async void CreateTask(NewTask newTask)
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

            await _taskRepository.Add(task);


        }
    }
}
