using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TaskMgmt.DataAccess;
using TaskMgmt.DataAccess.Repositories;

namespace TaskMgmt.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public string EncryptPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    sb.Append(hashedBytes[i]);
                }
                return sb.ToString();
            }
        }
        public bool VerifyPassword(string enteredPassword, string storedPasswordHash)
        {
            string enteredPasswordHash = EncryptPassword(enteredPassword);
            return string.Equals(storedPasswordHash, enteredPasswordHash);
        }
        public async Task<bool> Authenticate(string email, string enteredPassword)
        {
            var user = await _userRepository.GetByEmail(email);
            if (user != null)
            {
                return VerifyPassword(enteredPassword, user.PasswordHash);
            }
            else
            {
                return false;
            }
        }
    }
}
