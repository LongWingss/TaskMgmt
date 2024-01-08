using System;
using System.Collections.Generic;

namespace TaskMgmt.DataAccess.Models
{
    public partial class Invitation
    {
        public int InvitationId { get; set; }
        public int? GroupId { get; set; }
        public int InvitedByUser { get; set; }
        public string InviteeEmail { get; set; } = null!;
        public string? Token { get; set; }
        public bool? Status { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Group? Group { get; set; }
        public virtual User InvitedByUserNavigation { get; set; } = null!;
    }
}
