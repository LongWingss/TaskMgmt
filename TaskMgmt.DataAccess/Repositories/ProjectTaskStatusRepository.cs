using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMgmt.DataAccess.Models;

namespace TaskMgmt.DataAccess.Repositories
{
    public class ProjectTaskStatusRepository : IProjectTaskStatusRepository
    {
        private readonly TaskMgmntContext _dBcontext;
        public ProjectTaskStatusRepository(TaskMgmntContext dBcontext)
        {
            _dBcontext = dBcontext;
        }

        public async Task<IEnumerable<ProjectTaskStatus>> GetAll()
        {
            return await _dBcontext.ProjectTaskStatuses.ToListAsync();
        }

        public async Task<ProjectTaskStatus> GetById(int Id)
        {
            var status = await _dBcontext.ProjectTaskStatuses.Where(t => t.ProjectTaskStatusId == Id).SingleOrDefaultAsync();
            return status;
        }

        public async Task Add(ProjectTaskStatus status)
        {
            _dBcontext.ProjectTaskStatuses.Add(status);
            await _dBcontext.SaveChangesAsync();
        }
                            
    }
}
