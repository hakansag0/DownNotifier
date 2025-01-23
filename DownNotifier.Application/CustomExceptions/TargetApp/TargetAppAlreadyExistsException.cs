using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownNotifier.Application.CustomExceptions.TargetApp
{
    public class TargetAppAlreadyExistsException : CustomExceptionBase
    {
        public TargetAppAlreadyExistsException()
        {
        }

        public TargetAppAlreadyExistsException(string message)
            : base(message)
        {
        }

        public TargetAppAlreadyExistsException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
