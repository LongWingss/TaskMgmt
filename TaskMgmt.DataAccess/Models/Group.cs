using System;
using System.Collections.Generic;

namespace TaskMgmt.DataAccess.Models
{
    public partial class Group
    {
        public Group()
        {
            Invitations = new HashSet<Invitation>();
            Projects = new HashSet<Project>();
            UserGroups = new HashSet<UserGroup>();
        }

        public int GroupId { get; set; }
        public string GroupName { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }

        public virtual ICollection<Invitation> Invitations { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<UserGroup> UserGroups { get; set; }
    }
}
