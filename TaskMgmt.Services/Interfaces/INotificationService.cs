namespace TaskMgmt.Services.Interfaces
{

    public interface INotificationService
    {
        // void Notify(int recipientId, string subject, string message);
        Task NotifyAsync(string recipientId, string subject, string message);
        Task BulkNotifyAsync(IEnumerable<string> recipientIds, string subject, string message);
    }
}
