using DownNotifier.Application.CustomExceptions;
using DownNotifier.Application.CustomExceptions.User;
using DownNotifier.Application.Repositories;
using DownNotifier.Application.Utilities.UserPassword;
using DownNotifier.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownNotifier.Application.Features.UserFeatures.Command
{
    public class RegisterUserCommandRequest : IRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class RegisterUserCommandRequestHandler : IRequestHandler<RegisterUserCommandRequest>
    {
        private readonly IUserRepository userRepository;

        public RegisterUserCommandRequestHandler(IUserRepository userRepository) {
            this.userRepository = userRepository;
        }

        public Task Handle(RegisterUserCommandRequest request, CancellationToken cancellationToken)
        {
            //Check if user exists
            var user = userRepository.GetByEmail(request.Email);
            if (user!=null)
            {
                throw new RegisterUserExistsException($"You can not create account with email '{request.Email}'. Try different email address.");
            }

            PasswordHashHelper.CreatePasswordHashSHA512(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user = User.Create(request.Email, request.Name, passwordHash, passwordSalt);

            userRepository.Add(user);
            userRepository.SaveChanges();

            return Task.CompletedTask;
        }
    }
}
