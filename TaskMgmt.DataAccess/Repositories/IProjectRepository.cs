using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMgmt.DataAccess.Repositories
{
    public interface IProjectRepository
    {
        Task GetAll();
        Task Create();
        Task Edit();
        Task Delete();

    }
}
