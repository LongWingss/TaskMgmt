using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMgmt.DataAccess.Models;

namespace TaskMgmt.DataAccess.Repositories
{
    public class ProjectTaskRepository : IProjectTaskRepository
    {
        private readonly TaskMgmntContext _dBcontext;

        public ProjectTaskRepository(TaskMgmntContext dBcontext)
        {
            _dBcontext = dBcontext;
        }

        public async Task<ICollection<ProjectTask>> GetAll()
        {
            return await _dBcontext.ProjectTasks
                            .Include(e => e.Assignee)
                            .Include(e => e.Creator)
                            .Include(e => e.CurrentStatus)
                            .ToListAsync();
        }

        public async Task<ProjectTask?> GetById(int Id)
        {
            var task = await _dBcontext.ProjectTasks.Where(t => t.ProjectTaskId == Id)
                            .Include(e => e.Assignee)
                            .Include(e => e.Creator)
                            .Include(e => e.CurrentStatus)
                            .SingleOrDefaultAsync();
            return task;
        }

        public async Task Add(ProjectTask task)
        {
            _dBcontext.ProjectTasks.Add(task);
            await _dBcontext.SaveChangesAsync();
        }
    }
}
