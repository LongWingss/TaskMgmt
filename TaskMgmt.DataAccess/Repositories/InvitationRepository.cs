using Microsoft.EntityFrameworkCore;
using TaskMgmt.DataAccess.Models;

namespace TaskMgmt.DataAccess.Repositories
{
    public class InvitationRepository : IInvitationRepository
    {
        public TaskMgmntContext _context;
        public InvitationRepository(TaskMgmntContext context)
        {
            _context = context;
        }

        private readonly IInvitationRepository _inviteRepository;

        public async Task Enroll(Invitation invitation, string referralCode, UserGroup usergrp)
        {

            _context.Invitations.Add(invitation);
            _context.UserGroups.Add(usergrp);
            await _context.SaveChangesAsync();

        }

        public async Task<int> GetGroupIdFromReferralCode(string referralCode)
        {

            var group = await _context.Invitations
                .Where(rc => rc.Token == referralCode)
                .Select(rc => rc.GroupId)
                .FirstOrDefaultAsync();

            return (int)group;
        }

    }
}