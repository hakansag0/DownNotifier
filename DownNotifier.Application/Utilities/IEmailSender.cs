using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownNotifier.Application.Utilities
{
    public interface IEmailSender
    {
        public void SendEmail(string reciever, string subject, string body);
    }
}
