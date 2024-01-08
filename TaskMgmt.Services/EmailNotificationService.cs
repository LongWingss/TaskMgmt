using TaskMgmt.Services.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Net.Mail;

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

        async Task INotificationService.NotifyAsync(string recipientId, string subject, string message)
        {

            var text = new MimeMessage();
            text.From.Add(new MailboxAddress("TaskManagement", "taskmngement@gmail.com"));
            text.To.Add(new MailboxAddress("", recipientId));
            text.Subject = subject;
            text.Body = new TextPart("plain")
            {
                Text = message
            };

            using var client = new MailKit.Net.Smtp.SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 465, true); // Use ConnectAsync for async operation
            await client.AuthenticateAsync("taskmngement@gmail.com", "pfwz ljoc mwct difx"); // Use AuthenticateAsync
            await client.SendAsync(text); // Use SendAsync for async operation
            await client.DisconnectAsync(true); // Use DisconnectAsync

            Console.WriteLine("Email sent successfully.");


        }
    }
}
