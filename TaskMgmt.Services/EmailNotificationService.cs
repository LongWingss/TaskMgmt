using TaskMgmt.Services.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;

namespace TaskMgmt.Services
{

    public class EmailNotificationService : INotificationService
    {
        private readonly SmtpClient _client;

        private const string SenderEmail = "taskmngement@gmail.com";
        private const string SenderPassword = "pfwz ljoc mwct difx";

        public EmailNotificationService()
        {
            _client = new SmtpClient();
            _client.Connect("smtp.gmail.com", 465, true);
            _client.Authenticate(SenderEmail, SenderPassword);
        }

        public async Task BulkNotifyAsync(IEnumerable<string> recipientIds, string subject, string message)
        {
            var text = CreateEmailMessage(recipientIds, subject, message);
            await SendEmailAsync(text);
        }

        public async Task NotifyAsync(string recipientId, string subject, string message)
        {
            var text = CreateEmailMessage(new List<string> { recipientId }, subject, message);
            await SendEmailAsync(text);
        }

        private MimeMessage CreateEmailMessage(IEnumerable<string> recipientIds, string subject, string message)
        {
            var text = new MimeMessage();
            text.From.Add(new MailboxAddress("TaskManagement", SenderEmail));
            foreach (var recipientId in recipientIds)
            {
                text.To.Add(new MailboxAddress("", recipientId));
            }
            text.Subject = subject;
            text.Body = new TextPart("html")
            {
                Text = message
            };
            return text;
        }

        private async Task SendEmailAsync(MimeMessage message)
        {
            try
            {
                await _client.SendAsync(message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        ~EmailNotificationService()
        {
            _client.Disconnect(true);
        }
    }
}
