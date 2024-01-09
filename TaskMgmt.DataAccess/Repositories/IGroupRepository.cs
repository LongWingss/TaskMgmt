using TaskMgmt.DataAccess.Models;
namespace TaskMgmt.DataAccess.Repositories
{
    public interface IGroupRepository
    {
        public Task<Group> GetById(int id);
        public Task<Group[]> GetAll(int userid);
        public Task<int> Add(Group group);
        public Task<bool> CheckExists(string name);
        public Task<int> InviteUser(int userId, int groupId, string inviteeEmail);
        public Task<Invitation> GetInvitationByRefCode(string refCode);
    }
}