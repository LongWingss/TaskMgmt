using System;
using System.Collections.Generic;

namespace TaskMgmt.DataAccess.Models
{
    public partial class User
    {
        public User()
        {
            Invitations = new HashSet<Invitation>();
            Projects = new HashSet<Project>();
            UserGroups = new HashSet<UserGroup>();
        }

        public int UserId { get; set; }
        public string Email { get; set; } = null!;
        public string? PasswordHash { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual ICollection<Invitation> Invitations { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<UserGroup> UserGroups { get; set; }
    }
}
