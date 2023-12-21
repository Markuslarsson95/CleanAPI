using Application.Dtos;
using Domain.Models;
using MediatR;

namespace Application.Commands.Users.AddUser
{
    public sealed class AddUserCommand : IRequest<User>
    {
        public AddUserCommand(UserCreateDto newUser)
        {
            NewUser = newUser;
        }

        public UserCreateDto NewUser { get; }
    }
}
