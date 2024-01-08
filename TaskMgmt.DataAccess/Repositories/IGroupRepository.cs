using TaskMgmt.DataAccess.Models;
namespace TaskMgmt.DataAccess.Repositories
{
    public interface IGroupRepository
    {
        public Task<Group> Get();
        public Task<int> Add(Group group);
    }
}