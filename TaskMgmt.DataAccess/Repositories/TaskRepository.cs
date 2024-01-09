using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMgmt.DataAccess.Models;
using TaskMgmt.DataAccess.Repositories;

namespace TaskMgmt.DataAccess.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskMgmntContext _dBcontext;
        public TaskRepository(TaskMgmntContext dBcontext)
        {
            _dBcontext = dBcontext;
        }
        public async Task<ICollection<ProjectTask>> GetAll()
        {
            return await _dBcontext.ProjectTasks.ToListAsync();
        }
        public async Task<ProjectTask> GetById(int Id)
        {
            var task=await _dBcontext.ProjectTasks.Where(t=>t.ProjectTaskId== Id).SingleOrDefaultAsync();
            return task;

        }
        public async Task Add(ProjectTask task)
        {
            _dBcontext.ProjectTasks.Add(task);
            await _dBcontext.SaveChangesAsync();
        }
    }
}



