using Domain.Models.Animals;
using Infrastructure.Repositories.Birds;
using MediatR;
using Serilog;

namespace Application.Queries.Birds
{
    public class GetBirdByIdQueryHandler : IRequestHandler<GetBirdByIdQuery, Bird?>
    {
        private readonly IBirdRepository _birdRepository;

        public GetBirdByIdQueryHandler(IBirdRepository birdRepository)
        {
            _birdRepository = birdRepository;
        }

        public async Task<Bird?> Handle(GetBirdByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Log.Information($"Getting bird with ID {request.Id} from the repository");

                var wantedBird = await _birdRepository.GetById(request.Id);

                return wantedBird;
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"An error occurred while handling GetBirdByIdQuery for ID {request.Id}");
                throw new Exception($"Error occurred while handling GetBirdByIdQuery for ID {request.Id}", ex);
            }
        }
    }
}
