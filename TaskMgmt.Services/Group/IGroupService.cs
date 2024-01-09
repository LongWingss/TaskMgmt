using TaskMgmt.DataAccess.Models;

namespace TaskMgmt.Services
{
    public interface IGroupService
    {
        public Task<Group> GetById(int id);
        public Task<Group[]> GetAll(int userid);
        public Task<int> Add(Group group);
    }
}