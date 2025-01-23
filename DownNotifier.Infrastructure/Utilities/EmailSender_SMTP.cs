using DownNotifier.Application.Utilities;
using Microsoft.Extensions.Logging;
using System.Net.Mail;
using System.Net;

namespace DownNotifier.Infrastructure.Utilities
{
    public class EmailSender_SMTP : IEmailSender
    {
        private readonly SMTP_Values smtp_values;
        private readonly ILogger<EmailSender_SMTP> logger;

        public EmailSender_SMTP(SMTP_Values smtp_values, ILogger<EmailSender_SMTP> logger)
        {
            this.smtp_values = smtp_values;
            this.logger = logger;
        }

        public void SendEmail(string reciever, string subject, string body)
        {
            try
            {
                var senderAddress = new MailAddress(smtp_values.EmailAddress, smtp_values.EmailAddress);
                var recieverAddress = new MailAddress(reciever, reciever);
                var smtp = new SmtpClient
                {
                    Host = smtp_values.Host,
                    Port = smtp_values.Port,
                    Credentials = new NetworkCredential(senderAddress.Address, smtp_values.Password),
                    EnableSsl = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                };
                using var message = new MailMessage(senderAddress, recieverAddress)
                {
                    Subject = subject,
                    Body = body
                };
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error");
            }
        }
    }
}
