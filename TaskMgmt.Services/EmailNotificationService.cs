using TaskMgmt.Services.Interfaces;

namespace TaskMgmt.Services
{

    public class EmailNotificationService : INotificationService
    {

        public EmailNotificationService()
        {
            
        }

        Task INotificationService.BulkNotifyAsync(IEnumerable<string> recipientIds, string subject, string message)
        {
            throw new NotImplementedException();
        }

        Task INotificationService.NotifyAsync(string recipientId, string subject, string message)
        {
            throw new NotImplementedException();
        }
    }
}
