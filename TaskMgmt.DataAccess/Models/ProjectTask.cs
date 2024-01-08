namespace TaskMgmt.DataAccess.Models
{
    public class ProjectTask
    {
        public int TaskId { get; set; }
        public string Description { get; set; } = null!;
        public int ProjectId { get; set; }
        public int StatusId { get; set; }

        public TaskStatus TaskStatus { get; set; } = null!;
        public Project Project { get; set; } = null!;
    }
}
