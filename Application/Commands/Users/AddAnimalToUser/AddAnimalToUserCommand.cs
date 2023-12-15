using Application.Dtos;
using Domain.Models;
using MediatR;

namespace Application.Commands.Users.AddAnimalToUser
{
    public class AddAnimalToUserCommand : IRequest<User>
    {
        public AddAnimalToUserCommand(AnimalUserDto animalToUserRequest)
        {
            AnimalToUser = animalToUserRequest;
        }

        public AnimalUserDto AnimalToUser { get; }
    }
}
