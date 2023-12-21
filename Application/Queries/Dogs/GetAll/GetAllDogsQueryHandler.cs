using Application.Queries.Dogs.GetAll;
using Domain.Models.Animals;
using Infrastructure.Repositories.Dogs;
using MediatR;
using Serilog;

namespace Application.Queries.Dogs
{
    public class GetAllDogsQueryHandler : IRequestHandler<GetAllDogsQuery, List<Dog>>
    {
        private readonly IDogRepository _dogRepository;

        public GetAllDogsQueryHandler(IDogRepository dogRepository)
        {
            _dogRepository = dogRepository;
        }
        public Task<List<Dog>> Handle(GetAllDogsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Log.Information("Fetching all dogs from the repository");

                var dogList = _dogRepository.GetAll(request.SortyByBreed, request.SortByWeight);

                Log.Information("Successfully retrieved all dogs from the repository");

                return dogList;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while fetching all dogs from the repository");
                throw new Exception(ex.Message);
            }
        }
    }
}
