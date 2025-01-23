namespace DownNotifier.Application.CustomExceptions.User
{
    public class LoginUserNotFoundException : CustomExceptionBase
    {
        public LoginUserNotFoundException()
        {
        }

        public LoginUserNotFoundException(string message)
            : base(message)
        {
        }

        public LoginUserNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
