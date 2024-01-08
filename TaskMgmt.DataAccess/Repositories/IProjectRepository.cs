using TaskMgmt.DataAccess.Models;

namespace TaskMgmt.DataAccess.Repositories
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetAllAsync(int groupId);
        Task<Project?> GetByIdAsync(int groupId, int id);
        Task CreateAsync(int groupId, Project project);
        Task EditAsync(int groupId, int id, Project project);
        Task DeleteAsync(int groupId, int id);
    }
}
