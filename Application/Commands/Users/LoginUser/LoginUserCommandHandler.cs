using Domain.Models;
using Infrastructure.Database;
using MediatR;

namespace Application.Commands.Users.LoginUser
{
    public sealed class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, User>
    {
        private readonly MockDatabase _mockDatabase;

        public LoginUserCommandHandler(MockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }

        public Task<User> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var wantedUser = _mockDatabase.Users.FirstOrDefault(x => x.UserName == request.UserLogin.UserName);

            if (wantedUser == null || wantedUser.Password != request.UserLogin.Password)
            {
                return Task.FromResult<User>(null!);
            }

            return Task.FromResult(wantedUser);
        }
    }
}
