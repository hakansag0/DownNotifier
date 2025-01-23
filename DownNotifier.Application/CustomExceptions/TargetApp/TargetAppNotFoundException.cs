using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownNotifier.Application.CustomExceptions.TargetApp
{
    public class TargetAppNotFoundException : CustomExceptionBase
    {
        public TargetAppNotFoundException()
        {
        }

        public TargetAppNotFoundException(string message)
            : base(message)
        {
        }

        public TargetAppNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
