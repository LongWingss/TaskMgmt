using System.ComponentModel.DataAnnotations;

namespace TaskMgmt.DataAccess.Models
{
    public class TaskStatus
    {
        public int TaskStatusId { get; set; }
        public int ProjectId { get; set; }
        [MaxLength(20)]
        public string StatusText { get; set; } = null!;

        public Project Project { get; set; } = null!;
    }
}
