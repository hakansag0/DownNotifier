using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownNotifier.Application.CustomExceptions
{
    public abstract class CustomExceptionBase : Exception
    {
        public CustomExceptionBase()
        {
        }

        public CustomExceptionBase(string message)
            : base(message)
        {
        }

        public CustomExceptionBase(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
