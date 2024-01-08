using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMgmt.DataAccess.Models; 

namespace TaskMgmt.DataAccess.Repositories
{
    public interface IProjectRepository
    {
        Task<Project> GetAll();
        Task<Project> GetById(int id);
        Task Create(Project project);
        Task Edit(int id, Project project);
        Task Delete(int id);

    }
}
