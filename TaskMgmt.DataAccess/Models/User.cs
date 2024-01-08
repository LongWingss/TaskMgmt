using System;
using System.Collections.Generic;

namespace TaskMgmt.DataAccess.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public ICollection<Invitation> Invitations { get; set; } = null!;
        public ICollection<Project> Projects { get; set; } = null!;
        public ICollection<UserGroup> UserGroups { get; set; } = null!;
    }
}
