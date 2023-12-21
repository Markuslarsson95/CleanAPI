using Domain.Models.Animals;
using MediatR;

namespace Application.Queries.Dogs.GetAll
{
    public class GetAllDogsQuery : IRequest<List<Dog>>
    {
        public GetAllDogsQuery(string? sortyByBreed, int? sortByWeight)
        {
            SortyByBreed = sortyByBreed;
            SortByWeight = sortByWeight;
        }
        public string? SortyByBreed { get; }
        public int? SortByWeight { get; }
    }
}
