using Domain.Models.Animals;
using MediatR;

namespace Application.Commands.Cats
{
    public class DeleteCatByIdCommand : IRequest<Cat>
    {
        public DeleteCatByIdCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
