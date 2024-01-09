using TaskMgmt.DataAccess.Models;
using TaskMgmt.Services.DTOs;

namespace TaskMgmt.Services.ProjectTasks
{
    public class ProjectTaskService : IProjectTaskService
    {
        public IEnumerable<DataAccess.DTOs.ProjectTask> GetAll(int projectId)
        {
            throw new NotImplementedException();
        }

        public DataAccess.DTOs.ProjectTask Get(int taskId)
        {
            throw new NotImplementedException();
        }

        public void CreateTask(NewTask newTask)
        {
            throw new NotImplementedException();
        }
    }
}
