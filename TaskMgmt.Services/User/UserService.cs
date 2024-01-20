﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TaskMgmt.DataAccess;
using TaskMgmt.DataAccess.Repositories;
using TaskMgmt.Services.Helpers;
using TaskMgmt.Services.CustomExceptions;
using TaskMgmt.DataAccess.Models;
using TaskMgmt.DataAccess.UnitOfWork;
using System.Transactions;

namespace TaskMgmt.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IJwtHelper _jwtHelper;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserRepository userRepository, IGroupRepository groupRepository, IJwtHelper jwtHelper, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _groupRepository = groupRepository;
            _jwtHelper = jwtHelper;
            _unitOfWork = unitOfWork;
        }

        private string EncryptPassword(string password)
        {
            using SHA256 sha256 = SHA256.Create();
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashedBytes.Length; i++)
            {
                sb.Append(hashedBytes[i]);
            }
            return sb.ToString();
        }
        private bool VerifyPassword(string enteredPassword, string storedPasswordHash)
        {
            string enteredPasswordHash = EncryptPassword(enteredPassword);
            return string.Equals(storedPasswordHash, enteredPasswordHash);
        }
        public async Task<string> Authenticate(string email, string enteredPassword)
        {
            var user = await _userRepository.GetByEmail(email);
            if (user != null)
            {
                if (VerifyPassword(enteredPassword, user.PasswordHash))
                {

                    return _jwtHelper.GenerateToken(user.UserId);
                }
                else { throw new UnauthorizedAccessException("Invalid credentials"); }
            }
            else
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }
        }

        public async Task<string> SignUp(string email, string enteredPassword, string name, string groupName)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                bool exists = await _userRepository.UserExists(email);
                if (exists)
                {
                    throw new UserAlreadyExistsException("User already exists");
                }
                exists = await _groupRepository.CheckExists(groupName);
                if (exists)
                {
                    throw new GroupAlreadyExistsException("Group already exists");
                }

                int groupId = await _groupRepository.Add(new Group
                {
                    GroupName = groupName,
                });

                int userId = await _userRepository.Add(new User
                {
                    Username = name,
                    Email = email,
                    PasswordHash = EncryptPassword(enteredPassword),
                    DefaultGroupId = groupId,
                });
                await _userRepository.EnrollUserToGroup(userId, groupId, isAdmin: true);

                await _unitOfWork.CommitTransactionAsync();

                return _jwtHelper.GenerateToken(userId);
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackAsync();
                return e.Message;
            }
        }

        public async Task<string> SignUpWithReferral(string email, string enteredPassword, string name, string referralCode, string groupName)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                Invitation invitation = await _groupRepository.GetInvitationByRefCode(referralCode) ?? throw new Exception("Invitation not found");
                Group group = invitation.Group;

                if (email != invitation.InviteeEmail)
                {
                    throw new Exception("Invalid email");
                }
                bool exists = await _userRepository.UserExists(email);
                if (exists)
                {
                    throw new UserAlreadyExistsException("User already exists");
                }
                if (groupName != group.GroupName)
                {
                    throw new Exception("Invalid group name!");
                }

                if (invitation.Status)
                {
                    throw new Exception("User already enrolled!");
                }

                int userId = await _userRepository.Add(new User
                {
                    Username = name,
                    Email = email,
                    PasswordHash = EncryptPassword(enteredPassword),
                    DefaultGroupId = invitation.GroupId,
                });

                await _userRepository.EnrollUserToGroup(userId, (int)invitation.GroupId, isAdmin: false);
                invitation.Status = true;
                await _groupRepository.UpdateInvitation(invitation);
                await _unitOfWork.CommitTransactionAsync();

                return _jwtHelper.GenerateToken(userId);
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackAsync();
                return e.Message;
            }
        }

        public async Task<bool> IsUserInGroup(int userId, int groupId)
        {
            return await _userRepository.IsMember(userId, groupId);
        }

        public async Task<User> GetUserById(int userId)
        {
            return await _userRepository.GetById(userId);
        }
    }
}