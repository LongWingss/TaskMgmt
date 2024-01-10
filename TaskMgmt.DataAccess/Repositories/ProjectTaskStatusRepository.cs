using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
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
            return await _dBcontext.ProjectTaskStatuses
                            .Include(e => e.Project)
                            .ToListAsync();
        }

        public async Task<ProjectTaskStatus?> GetById(int Id)
        {
            var status = await _dBcontext.ProjectTaskStatuses.Where(t => t.ProjectTaskStatusId == Id).Include(e => e.ProjectId).SingleOrDefaultAsync();
            return status;
        }

        public async Task Add(ProjectTaskStatus status)
        {
            _dBcontext.ProjectTaskStatuses.Add(status);
            await _dBcontext.SaveChangesAsync();
        }

        public async Task InitProjectStatus(int projectId, Dictionary<string, string>? statusColorPairs = null)
        {
            var DefaultOptions = new Dictionary<string, string>
            {
                { "open", "#FF0000" },
                { "in progress", "#FFFF00" },
                { "completed", "#00FF00" }
            };

            if (statusColorPairs is null)
            {
                statusColorPairs = DefaultOptions;
            }

            foreach (var (status, color) in statusColorPairs)
            {
                await _dBcontext.AddAsync(new ProjectTaskStatus{
                    ProjectId = projectId,
                    StatusText = status,
                    StatusColor = color,
                });
            }
        }
    }
}
