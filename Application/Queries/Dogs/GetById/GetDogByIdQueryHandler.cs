using Domain.Models.Animals;
using Infrastructure.Repositories.Dogs;
using MediatR;
using Serilog;

namespace Application.Queries.Dogs.GetById
{
    public class GetDogByIdQueryHandler : IRequestHandler<GetDogByIdQuery, Dog?>
    {
        private readonly IDogRepository _dogRepository;

        public GetDogByIdQueryHandler(IDogRepository dogRepository)
        {
            _dogRepository = dogRepository;
        }

        public Task<Dog?> Handle(GetDogByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Log.Information("GetById in dogRepository called");
                var wantedDog = _dogRepository.GetById(request.Id);

                return wantedDog;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An unexpected error occurred.");
                throw new Exception(ex.Message);
            }
        }
    }
}
