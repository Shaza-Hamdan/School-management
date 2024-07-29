using System.Net;
using System.Net.Mail;
using TRIAL.Services;
using Microsoft.Extensions.Configuration;
using TRIAL.Persistence;
using Microsoft.Extensions.Options;
public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;

    public EmailService(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public void SendEmail(string to, string subject, string body)
    {
        var smtpClient = new SmtpClient(_emailSettings.SmtpServer)
        {
            Port = _emailSettings.SmtpPort,
            Credentials = new NetworkCredential(_emailSettings.SmtpUsername, _emailSettings.SmtpPassword),
            EnableSsl = true,
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(_emailSettings.FromEmail),
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