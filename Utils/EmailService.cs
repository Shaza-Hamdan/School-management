using System.Net;
using System.Net.Mail;
using TRIAL.Services;
using Microsoft.Extensions.Configuration;
using TRIAL.Persistence;
using Microsoft.Extensions.Options;
public class EmailService : IEmailService
{
    private readonly EmailSettings emailsettings;

    public EmailService(IOptions<EmailSettings> emailSettings)
    {
        emailsettings = emailSettings.Value;
    }

    public void SendEmail(string to, string subject, string body)
    {
        var smtpClient = new SmtpClient(emailsettings.SmtpServer)
        {
            Port = emailsettings.SmtpPort,
            Credentials = new NetworkCredential(emailsettings.SmtpUsername, emailsettings.SmtpPassword),
            EnableSsl = true,
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(emailsettings.FromEmail),
            Subject = subject,
            Body = body,
            IsBodyHtml = true,
        };
        mailMessage.To.Add(to);

        smtpClient.Send(mailMessage);
    }
}
/*
using System;

namespace TRIAL.Services
{
    public class EmailService : IEmailService
    {
        public void SendEmail(string to, string subject, string body)
        {
            // Implementation for sending an email, e.g., using SMTP
            Console.WriteLine($"Sending email to {to} with subject {subject}");
            // Actual email sending code would go here
        }
    }
}*/