using TaskMgmt.DataAccess.Models;

namespace TaskMgmt.DataAccess.Repositories
{
    public interface IInvitationRepository
    {
        public Task Enroll(Invitation invitation, string referralCode, UserGroup usergrp);
        // public Task InviteUSer(Invitation invitation);
        public Task<int> GetGroupIdFromReferralCode(string referralCode);
    }
}