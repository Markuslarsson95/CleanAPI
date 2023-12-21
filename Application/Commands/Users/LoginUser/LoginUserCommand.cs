using Application.Dtos;
using MediatR;

namespace Application.Commands.Users.LoginUser
{
    public sealed class LoginUserCommand : IRequest<string>
    {
        public LoginUserCommand(LoginDto loginDto)
        {
            UserLogin = loginDto;
        }

        public LoginDto UserLogin { get; }
    }
}
