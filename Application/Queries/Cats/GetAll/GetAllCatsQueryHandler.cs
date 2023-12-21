using Domain.Models.Animals;
using Infrastructure.Repositories.Cats;
using MediatR;
using Serilog;

namespace Application.Queries.Cats.GetAll
{
    public class GetAllCatsQueryHandler : IRequestHandler<GetAllCatsQuery, List<Cat>>
    {
        private readonly ICatRepository _catRepository;

        public GetAllCatsQueryHandler(ICatRepository catRepository)
        {
            _catRepository = catRepository;
        }
        public Task<List<Cat>> Handle(GetAllCatsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Log.Information("Fetching all cats from the repository");

                var catList = _catRepository.GetAll(request.SortyByBreed, request.SortByWeight);

                Log.Information("Successfully retrieved all cats from the repository");

                return catList;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while fetching all dogs from the repository");
                throw new Exception(ex.Message);
            }
        }
    }
}
