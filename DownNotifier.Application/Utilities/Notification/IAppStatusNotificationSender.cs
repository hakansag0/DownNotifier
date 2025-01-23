using DownNotifier.Application.Features.TargetAppFeatures.Query;
using DownNotifier.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownNotifier.Application.Utilities.Notification
{
    public interface IAppStatusNotificationSender
    {
        public void SendNotification(TargetApp targetApp, string message);
    }
}
