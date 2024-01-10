using System;
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

namespace TaskMgmt.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;

        public UserService(IUserRepository userRepository, IGroupRepository groupRepository)
        {
            _userRepository = userRepository;
            _groupRepository = groupRepository;
        }

        public string EncryptPassword(string password)
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
        public bool VerifyPassword(string enteredPassword, string storedPasswordHash)
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
                    var jwtHelper = new JwtHelper();
                    return jwtHelper.GenerateToken(user.UserId);
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
            bool exists = await _userRepository.UserExists(email);
            if (exists)
            {
                throw new UserAlreadyExistsException("User already exists");
            }
            exists = await _groupRepository.CheckExists(groupName);
            if (exists)
            {
                throw new Exception("Group already exists");
            }


            int groupId = await _groupRepository.Add(new Group
            {
                GroupName = groupName,
            });

            int userId = await _userRepository.Add(new User
            {
                Email = email,
                PasswordHash = EncryptPassword(enteredPassword),
                DefaultGroupId = groupId,
            });

            await _userRepository.EnrollUserToGroup(userId, groupId, isAdmin: true);

            var jwtHelper = new JwtHelper();
            return jwtHelper.GenerateToken(userId);
        }

        public async Task<bool> IsUserInGroup(int userId, int groupId)
        {
            return await _userRepository.IsMember(userId, groupId);
        }
    }
}