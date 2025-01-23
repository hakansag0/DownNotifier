using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownNotifier.Infrastructure.Utilities
{
    public class SMTP_Values
    {
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public string EmailAddress { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public SMTP_Values(string host, int port, string emailAddress, string password)
        {
            Host = host;
            Port = port;
            EmailAddress = emailAddress;
            Password = password;
        }
    }
}
