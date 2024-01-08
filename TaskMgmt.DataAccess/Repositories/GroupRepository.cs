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
        public async Task<Group> Add(Group group)
        {
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();
            return group;
        }
        public async Task<Group> GetById(int id)
        {
            return await _context.Groups.FindAsync(id);
        }
        public async Task<Group[]> GetAll()
        {
            return await _context.Groups.ToArrayAsync();
        }
    }
}