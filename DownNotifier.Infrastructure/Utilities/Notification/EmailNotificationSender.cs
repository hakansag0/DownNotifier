using DownNotifier.Application.Features.TargetAppFeatures.Query;
using DownNotifier.Application.Repositories;
using DownNotifier.Application.Utilities;
using DownNotifier.Application.Utilities.Notification;
using DownNotifier.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownNotifier.Infrastructure.Utilities.Notification
{
    public class EmailNotificationSender : IAppStatusNotificationSender
    {
        private readonly IUserRepository userRepository;
        private readonly IEmailSender emailSender;

        public EmailNotificationSender(IUserRepository userRepository, IEmailSender emailSender) {
            this.userRepository = userRepository;
            this.emailSender = emailSender;
        }
        public void SendNotification(TargetApp targetApp, string message)
        {
            User? user = userRepository.Get(s => s.Id == targetApp.UserId);
            if(user == null)
            {
                throw new NullReferenceException("User not found.");
            }

            emailSender.SendEmail(user.Email, $"Down Notifier - {targetApp.Name} is not healthy", $"Application {targetApp.Name} ({targetApp.URL}) is not healhty at {targetApp.LastCheckDate:yyyy/MM/dd HH:mm:ss} UTC {(string.IsNullOrEmpty(message) ? string.Empty : $" with message '{message}'")}");
        }
    }
}
