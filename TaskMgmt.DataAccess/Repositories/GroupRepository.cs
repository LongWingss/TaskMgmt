using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMgmt.DataAccess.Models;

namespace TaskMgmt.DataAccess.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly TaskMgmntContext _context;
        public GroupRepository(TaskMgmntContext context)
        {
            _context = context;
        }
        public async Task<int> Add(Group group)
        {
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();
            return group.GroupId;
        }
        public async Task<Group> GetById(int id)
        {
            return await _context.Groups.FindAsync(id);
        }
        public async Task<Group[]> GetAll(int userid)
        {
            // return await _context.Groups.ToArrayAsync();
            var userGroups = await _context.UserGroups.Include(ug => ug.Group).Where(ug => ug.UserId == userid).ToArrayAsync();
            return userGroups.Select(ug => ug.Group).ToArray();
        }

        public async Task<bool> CheckExists(string name)
        {
            return await _context.Groups.AnyAsync(g => g.GroupName == name);
        }
        public int GenerateRefCode()
        {
            var random = new Random();
            return random.Next(100000, 999999);
        }
        public async Task<int> InviteUser(int userId, int groupId, string inviteeEmail)
        {

            //TODO : Precheck if there is already an exisiting invitaion for the email (check the expiration)
            var refcode = GenerateRefCode();
            var invitation = new Invitation
            {
                GroupId = groupId,
                InvitedByUser = userId,
                InviteeEmail = inviteeEmail,
                Token = refcode.ToString(),
                CreatedAt = DateTime.UtcNow
            };
            _context.Invitations.Add(invitation);
            await _context.SaveChangesAsync();
            return userId;
        }
        public Task<Invitation> GetInvitationByRefCode(string refCode)
        {
            var invitation = _context.Invitations.FirstOrDefaultAsync(x => x.Token == refCode);
            return invitation;
        }

        public async Task Enroll(Invitation invitation, string referralCode, UserGroup usergrp)
        {

            _context.Invitations.Add(invitation);
            _context.UserGroups.Add(usergrp);
            await _context.SaveChangesAsync();

        }

        //public async Task<int> GetGroupIdFromReferralCode(string referralCode)
        //{

        //    var group = await _context.Invitations
        //        .Where(rc => rc.Token == referralCode)
        //        .Select(rc => rc.GroupId)
        //        .FirstOrDefaultAsync();

        //    return (int)group;
        //}
    }
}