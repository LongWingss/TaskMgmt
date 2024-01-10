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
using TaskMgmt.Services.Interfaces;

namespace TaskMgmt.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;
        private readonly INotificationService _notificationService;

        public GroupService(IGroupRepository groupRepository, INotificationService notificationService, IUserRepository userRepository)
        {
            _groupRepository = groupRepository;
            _notificationService = notificationService;
            _userRepository = userRepository;
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

        public async Task Enroll(int userId, int groupId, string invitation)
        {
            var enrollment = new UserGroup
            {
                GroupId = groupId,
                UserId = userId,
                IsAdmin = false,
            };
            await _groupRepository.Enroll(enrollment);
        }
        public async Task<int> InviteUser(int userId, int groupId, string inviteeEmail)
        {
            try
            {
                Invitation invitation = await _groupRepository.InviteUser(userId, groupId, inviteeEmail);
                var group = await _groupRepository.GetById(groupId);
                User user = await _userRepository.GetById(userId);
                string subject = $"{user.Email} invited you to join the Group {group.GroupName}";
                string message = $"Please use this code to join the Group: {invitation.Token}";
                await _notificationService.NotifyAsync(inviteeEmail, subject, message);
                return invitation.InvitationId;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
