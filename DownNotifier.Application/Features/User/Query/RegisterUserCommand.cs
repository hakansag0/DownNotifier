using DownNotifier.Application.CustomExceptions.User;
using DownNotifier.Application.Repositories;
using DownNotifier.Application.Utilities.UserPassword;
using MediatR;

namespace DownNotifier.Application.Features.UserFeatures.Query
{
    public class LoginUserResponse
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
    public class LoginUserRequest : IRequest<LoginUserResponse>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class LoginUserRequestHandler : IRequestHandler<LoginUserRequest, LoginUserResponse>
    {
        private readonly IUserRepository userRepository;

        public LoginUserRequestHandler(IUserRepository userRepository) {
            this.userRepository = userRepository;
        }

        public Task<LoginUserResponse> Handle(LoginUserRequest request, CancellationToken cancellationToken)
        {
            //Check if user exists
            var user = userRepository.GetByEmail(request.Email);
            if (user==null || !PasswordHashHelper.VerifyPasswordHashSHA512(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                throw new LoginUserNotFoundException("User not found.");
            }

            return Task.FromResult(new LoginUserResponse()
            {
                Id = user.Id,
                Email = user.Email,
                Name=user.Name,
            });
        }
    }
}
