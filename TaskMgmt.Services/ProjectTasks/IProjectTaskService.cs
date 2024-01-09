using TaskMgmt.DataAccess.Models;
using TaskMgmt.Services.DTOs;

namespace TaskMgmt.Services.ProjectTasks
{
    public interface IProjectTaskService
    {
        public Task<ProjectTask> CreateTask(int projectId, NewTaskDto newTask);
        public Task<ProjectTaskDto?> Get(int taskId);
        public Task<IEnumerable<ProjectTaskDto>> GetAll(int projectId);
    }
}
