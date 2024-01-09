namespace TaskMgmt.Services.DTOs
{
    public class NewTask
    {

        public string Description { get; set; } = null!;
        public DateTime DueDate { get; set; } 
        public string CreatedBy { get; set; } = null!;
        public string CurrentStatus { get; set; } = null!;

        public int AssigneeId { get; set; }
        public int CreatorId { get; set; }
        public int ProjectId { get; set; }
        public int CurrentStatusId { get; set; }
    }
}
