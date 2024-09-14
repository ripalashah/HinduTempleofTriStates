using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;

namespace HinduTempleofTriStates.Services
{
    public class EmailSettings
    {
        public required string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public required string SenderEmail { get; set; }
        public required string SenderPassword { get; set; }
        public required string SenderName { get; set; }
    }

    public class EmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }
        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                var mail = new MailMessage
                {
                    From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };
                mail.To.Add(toEmail);

                using (var smtpClient = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort))
                {
                    smtpClient.Credentials = new NetworkCredential(_emailSettings.SenderEmail, _emailSettings.SenderPassword);

                    // Important: Yahoo requires SSL
                    smtpClient.EnableSsl = true;

                    // Ensure that authentication is required
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                    await smtpClient.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                // Log error details for debugging
                throw new InvalidOperationException("Failed to send email. Check SMTP settings or network connection.", ex);
            }
        }


    }
}
