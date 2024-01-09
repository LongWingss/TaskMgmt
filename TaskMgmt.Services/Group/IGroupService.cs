using TaskMgmt.DataAccess.Models;

namespace TaskMgmt.Services
{
    public interface IGroupService
    {
        public Task<Group> GetById(int id);
        public Task<Group[]> GetAll();
        public Task<Group> Add(Group group);
    }
}