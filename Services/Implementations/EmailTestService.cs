using System.Net;
using System.Net.Mail;
using TRIAL.Services;
using TRIAL.Persistence;
using Microsoft.Extensions.Options;
public class EmailTestService : IEmailTestService
{
    private readonly EmailSettings emailsettings;

    public EmailTestService(IOptions<EmailSettings> emailSettings)
    {
        emailsettings = emailSettings.Value;
    }



    public async Task SendEmail(string to, string subject, string body)
    {
        try
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

        catch (SmtpException ex)
        {
            // Handle general SMTP exceptions, like server issues
            Console.WriteLine($"SMTP error: {ex.Message}");
            throw;
        }

        catch (Exception ex)
        {
            // Handle any other exception that might occur
            Console.WriteLine($"Unexpected error: {ex.Message}");
            throw;
        }
    }
}
