using Domain.Models.Animals;
using MediatR;

namespace Application.Queries.Birds.GetAll
{
    public class GetAllBirdsQuery : IRequest<List<Bird>>
    {
        public GetAllBirdsQuery(string? sortyByColor)
        {
            SortyByColor = sortyByColor;
        }
        public string? SortyByColor { get; }
    }
}
