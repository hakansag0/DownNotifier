using DownNotifier.Application.Utilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownNotifier.Infrastructure.Utilities
{
    public class EmailSender_ConsoleLogger : IEmailSender
    {
        private readonly ILogger<EmailSender_ConsoleLogger> logger;

        public EmailSender_ConsoleLogger(ILogger<EmailSender_ConsoleLogger> logger)
        {
            this.logger = logger;
        }

        public void SendEmail(string reciever, string subject, string body)
        {
            logger.LogWarning($"Email Sender > Reciever:{reciever} > Subject:{subject} > Body:{body}");
        }
    }
}
