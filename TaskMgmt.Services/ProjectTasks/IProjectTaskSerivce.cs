using TaskMgmt.DataAccess.DTOs;
using TaskMgmt.Services.DTOs;

namespace TaskMgmt.Services.ProjectTasks
{
    public interface IProjectTaskService
    {
        public IEnumerable<ProjectTask> GetAll(int projectId);
        public ProjectTask Get(int taskId);
        public void CreateTask(NewTask newTask);
    }
}
