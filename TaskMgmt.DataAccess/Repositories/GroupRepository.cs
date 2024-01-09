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
    }
}