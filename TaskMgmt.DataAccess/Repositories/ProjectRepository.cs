using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task Create(Project project)
        {
            await _dbcontext.AddAsync(project);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            if (id == null)
            {
                throw new ArgumentNullException($"Project with id {id} not found.");
            }
            var project = await GetById(id);
            if (project == null)
            {
                throw new InvalidOperationException("Project not found");
            }
            _dbcontext.Remove(project);
            await _dbcontext.SaveChangesAsync();
        }

        public Task Edit(int id, Project project)
        {
            throw new NotImplementedException();
        }

        public Task<Project> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Project> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
