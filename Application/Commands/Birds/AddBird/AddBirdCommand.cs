using Application.Dtos;
using Domain.Models.Animals;
using MediatR;

namespace Application.Commands.Birds
{
    public class AddBirdCommand : IRequest<Bird>
    {
        public AddBirdCommand(BirdDto newBird)
        {
            NewBird = newBird;
        }

        public BirdDto NewBird { get; }
    }
}
