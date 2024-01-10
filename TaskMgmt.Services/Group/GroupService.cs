using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TaskMgmt.DataAccess;
using TaskMgmt.DataAccess.Models;
using TaskMgmt.DataAccess.Repositories;
using TaskMgmt.Services.CustomExceptions;
using TaskMgmt.Services.Helpers;

namespace TaskMgmt.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;

        public GroupService(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<Group> GetById(int id)
        {
            var group = await _groupRepository.GetById(id);
            if (group == null)
            {
                throw new GroupNotFoundException("Group Not Found");
            }
            return group;
        }

        public async Task<Group[]> GetAll(int userid)
        {
            var groups = await _groupRepository.GetAll(userid);
            return groups;
        }

        public async Task<int> Add(Group group)
        {
            bool exists = await _groupRepository.CheckExists(group.GroupName);
            if (exists)
            {
                throw new GroupAlreadyExistsException("Group already exists");
            }
            else
            {
                await _groupRepository.Add(group);
                return group.GroupId;
            }
        }

        public async Task<Invitation> Enroll(Invitation invitation, string referralCode, int id)
        {
            var group = await _groupRepository.GetInvitationByRefCode(referralCode);

            var enroll = new Invitation
            {
                GroupId = group.GroupId,
                InvitedByUser = invitation.InvitedByUser,
                InviteeEmail = invitation.InviteeEmail,
                Token = referralCode,
                CreatedAt = DateTime.Now,

            };

            var usergrp = new UserGroup
            {
                UserId = id,
                GroupId = (int)group.GroupId,
                IsAdmin = false
            };

            await _groupRepository.Enroll(invitation, referralCode, usergrp);
            return enroll;
        }
        public async Task<int> InviteUser(int userId, int groupId, string inviteeEmail)
        {
            var invitationId = await _groupRepository.InviteUser(userId, groupId, inviteeEmail);
            return invitationId;
        }
        }
}
