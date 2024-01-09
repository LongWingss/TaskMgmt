using TaskMgmt.DataAccess.Models;

namespace TaskMgmt.Services
{
    public interface IGroupService
    {
         Task<Group> GetById(int id);
         Task<Group[]> GetAll(int userid);
         Task<int> Add(Group group);
        Task<Invitation> Enroll(Invitation invitation, string referralCode, int id);
    }
}