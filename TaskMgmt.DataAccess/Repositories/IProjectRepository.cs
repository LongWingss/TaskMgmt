using TaskMgmt.DataAccess.Models;

namespace TaskMgmt.DataAccess.Repositories
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetAllAsync(int groupId);
        Task<Project?> GetByIdAsync(int groupId, int id);
        Task CreateAsync( Project project);
        Task EditAsync(  Project project);
        Task DeleteAsync(int groupId, int id);
    }
}
