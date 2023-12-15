using Application.Dtos;
using Domain.Models;
using MediatR;

namespace Application.Commands.Users.RemoveAnimalFromUser
{
    public class RemoveAnimalFromUserCommand : IRequest<User>
    {
        public RemoveAnimalFromUserCommand(AnimalUserDto removeAnimalFromUserRequest)
        {
            RemoveAnimalFromUser = removeAnimalFromUserRequest;
        }

        public AnimalUserDto RemoveAnimalFromUser { get; }
    }
}
