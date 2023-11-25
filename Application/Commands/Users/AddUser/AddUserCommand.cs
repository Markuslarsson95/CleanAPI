using Application.Dtos;
using Domain.Models;
using MediatR;

namespace Application.Commands.Users.AddUser
{
    public sealed class AddUserCommand : IRequest<User>
    {
        public AddUserCommand(UserDto newUser)
    {
        NewUser = newUser;
    }

    public UserDto NewUser { get; }
    }
}
