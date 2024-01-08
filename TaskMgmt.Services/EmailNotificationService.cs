using TaskMgmt.Services.Interfaces;

namespace TaskMgmt.Services
{

    public class EmailNotificationService : INotificationService
    {

        public EmailNotificationService()
        {

        }

        public Task BulkNotifyAsync(IEnumerable<string> recipientIds, string subject, string message)
        {
            throw new NotImplementedException();
        }

        public Task NotifyAsync(string recipientId, string subject, string message)
        {
            throw new NotImplementedException();
        }
    }
}
