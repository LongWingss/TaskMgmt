using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMgmt.DataAccess.Models;

namespace TaskMgmt.DataAccess.Repositories
{
    internal class TaskRepository : ITaskRepository
    {
        private readonly TaskMgmntContext _context;
        public TaskRepository(TaskMgmntContext context)
        {
            _context = context;
        }
        public Task<ProjectTask> Add(ProjectTask task)
        {
            throw new NotImplementedException();
        }

        public Task<ProjectTask[]> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<ProjectTask> GetById(int Id)
        {
            return await _context.ProjectTasks.FindAsync(Id);
            
        }
    }
}
