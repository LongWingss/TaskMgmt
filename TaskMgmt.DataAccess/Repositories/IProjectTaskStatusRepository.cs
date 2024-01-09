using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMgmt.DataAccess.Models;

namespace TaskMgmt.DataAccess.Repositories
{
    public interface IProjectTaskStatusRepository
    {
        public Task<IEnumerable<ProjectTaskStatus>> GetAll();
        public Task<ProjectTaskStatus> GetById(int id);
        public Task Add(ProjectTaskStatus status);


    }
}
