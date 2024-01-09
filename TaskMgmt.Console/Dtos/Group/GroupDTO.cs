using System;
using System.Collections.Generic;

namespace TaskMgmt.Console.Dtos.Group
{
    public class GroupDTO
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        //public ICollection<Invitation> Invitations { get; set; } = null!;
        //public ICollection<Project> Projects { get; set; } = null!;
        //public ICollection<UserGroup> UserGroups { get; set; } = null!;
    }
}
