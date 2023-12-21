using Domain.Models.Animals;
using Infrastructure.Repositories.Birds;
using MediatR;
using Serilog;

namespace Application.Queries.Birds.GetAll
{
    public class GetAllBirdsQueryHandler : IRequestHandler<GetAllBirdsQuery, List<Bird>>
    {
        private readonly IBirdRepository _birdRepository;

        public GetAllBirdsQueryHandler(IBirdRepository birdRepository)
        {
            _birdRepository = birdRepository;
        }
        public async Task<List<Bird>> Handle(GetAllBirdsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Log.Information("Getting all birds from the repository");

                var birdList = await _birdRepository.GetAll(request.SortyByColor);

                Log.Information("Successfully retrieved bird list from the repository");

                return birdList;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while handling GetAllBirdsQuery");
                throw new Exception("Error occurred while handling GetAllBirdsQuery", ex);
            }
        }
    }
}
