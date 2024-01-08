using System;
using System.Collections.Generic;

namespace TaskMgmt.DataAccess.Models
{
    public partial class Project
    {
        public int ProjectId { get; set; }
        public int? GroupId { get; set; }
        public string ProjectName { get; set; } = null!;
        public string ProjectDescription { get; set; } = null!;
        public int OwnerId { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Group? Group { get; set; }
        public virtual User Owner { get; set; } = null!;
    }
}
