using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMgmt.DataAccess.Models;

namespace TaskMgmt.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        //context
        private readonly TaskMgmntContext _taskMgmntContext;
        public UserRepository(TaskMgmntContext taskMgmntContext)
        {
            _taskMgmntContext = taskMgmntContext;
        }

        public async Task<IEnumerable<User>> Get()
        {
            var users = await _taskMgmntContext.Users.ToListAsync();
            return users;
        }
        public async Task<int> Add(User user)
        {
            _taskMgmntContext.Users.Add(user);
            await _taskMgmntContext.SaveChangesAsync();
            return user.UserId;
        }
        public async Task<User> GetByEmail(string email)
        {
            var user = await _taskMgmntContext.Users.FirstOrDefaultAsync(user => user.Email == email);
            return user;
        }

        public async Task<bool> UserExists(string email)
        {
            return await _taskMgmntContext.Users.AnyAsync(u => u.Email == email);
        }
    }
}
