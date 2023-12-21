using Domain.Models.Animals;
using MediatR;

namespace Application.Queries.Cats.GetAll
{
    public class GetAllCatsQuery : IRequest<List<Cat>>
    {
        public GetAllCatsQuery(string? sortyByBreed, int? sortByWeight)
        {
            SortyByBreed = sortyByBreed;
            SortByWeight = sortByWeight;
        }
        public string? SortyByBreed { get; }
        public int? SortByWeight { get; }
    }
}
