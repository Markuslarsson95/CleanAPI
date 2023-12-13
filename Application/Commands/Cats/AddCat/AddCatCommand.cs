using Application.Dtos;
using Domain.Models.Animals;
using MediatR;

namespace Application.Commands.Cats
{
    public class AddCatCommand : IRequest<Cat>
    {
        public AddCatCommand(CatDto newCat)
        {
            NewCat = newCat;
        }

        public CatDto NewCat { get; }
    }
}
