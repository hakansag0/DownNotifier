using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DownNotifier.Application.CustomExceptions.User
{
    public class RegisterUserExistsException : CustomExceptionBase
    {
        public RegisterUserExistsException()
        {
        }

        public RegisterUserExistsException(string message)
            : base(message)
        {
        }

        public RegisterUserExistsException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
