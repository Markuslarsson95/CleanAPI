using Application.Dtos;
using MediatR;

namespace Application.Commands.Users.LoginUser
{
    public sealed class LoginUserCommand : IRequest<string>
    {
        public LoginUserCommand(UserDto dtoLogin)
        {
            UserLogin = dtoLogin;
        }

        public UserDto UserLogin { get; }
    }
}
