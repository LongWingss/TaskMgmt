using Microsoft.EntityFrameworkCore;
using TaskMgmt.DataAccess.Models;

namespace TaskMgmt.DataAccess.Repositories
{

    public class ProjectRepository : IProjectRepository
    {
        private readonly TaskMgmntContext _dbcontext;
        public ProjectRepository(TaskMgmntContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<IEnumerable<Project>> GetAllAsync(int groupId)
        {
            return await _dbcontext.Projects.Where(p => p.GroupId == groupId).ToListAsync();
        }

        public async Task<Project?> GetByIdAsync(int groupId, int id)
        {
            return await _dbcontext.Projects.FirstOrDefaultAsync(p => p.GroupId == groupId && p.ProjectId == id);
        }

        public async Task CreateAsync(int groupId, Project project)
        {
            if (project.GroupId != groupId)
            {
                throw new ArgumentException("Mismatched Group Id.");
            }
            _dbcontext.Projects.Add(project);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task EditAsync(int groupId, int id, Project project)
        {
            if (id != project.ProjectId || groupId != project.GroupId)
            {
                throw new ArgumentException("Mismatched ID or Group ID.");
            }

            _dbcontext.Entry(project).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int groupId, int id)
        {
            var project = await _dbcontext.Projects.FirstOrDefaultAsync(p => p.GroupId == groupId && p.ProjectId == id);
            if (project == null)
            {
                throw new InvalidOperationException("Project not found.");
            }

            _dbcontext.Projects.Remove(project);
            await _dbcontext.SaveChangesAsync();
        }

    }
}
