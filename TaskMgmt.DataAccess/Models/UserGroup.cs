using System;
using System.Collections.Generic;

namespace TaskMgmt.DataAccess.Models
{
    public class UserGroup
    {
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public bool? IsAdmin { get; set; }

        public Group Group { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
