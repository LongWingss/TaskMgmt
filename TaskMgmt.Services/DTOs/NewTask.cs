namespace TaskMgmt.Services.DTOs
{
    public class NewTask
    {
        
        public string Description { get; set; } 
        public DateTime DueDate { get; set; } 
        public string CreatedBy { get; set; } 
        public string CurrentStatus { get; set; } 

        public int AssigneeId { get; set; }
        public int CreatorId { get; set; }
        public int ProjectId { get; set; }
        public int CurrentStatusId { get; set; }
    }
}
