using TaskMgmt.DataAccess.Models;
namespace TaskMgmt.DataAccess.Repositories
{
    public interface IGroupRepository
    {
        public Task<Group> GetById(int id);
        public Task<Group[]> GetAll();
        public Task<Group> Add(Group group);
        public Task<bool> CheckExists(string name);
    }
}