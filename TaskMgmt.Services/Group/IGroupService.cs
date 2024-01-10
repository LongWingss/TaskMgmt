using TaskMgmt.DataAccess.Models;

namespace TaskMgmt.Services
{
    public interface IGroupService
    {
        Task<Group> GetById(int id);
        Task<Group[]> GetAll(int userid);
        Task<int> Add(Group group);
        public Task<int> InviteUser(int userId, int groupId, string inviteeEmail);
        Task<Invitation> Enroll(Invitation invitation, string referralCode, int id);
    }
}