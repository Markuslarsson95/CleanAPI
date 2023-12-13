using Domain.Models.Animal;
using Infrastructure.Repositories;
using MediatR;

namespace Application.Queries.Birds
{
    public class GetBirdByIdQueryHandler : IRequestHandler<GetBirdByIdQuery, Bird?>
    {
        private readonly IBirdRepository _birdRepository;

        public GetBirdByIdQueryHandler(IBirdRepository birdRepository)
        {
            _birdRepository = birdRepository;
        }

        public Task<Bird?> Handle(GetBirdByIdQuery request, CancellationToken cancellationToken)
        {
            var wantedBird = _birdRepository.GetById(request.Id);

            if (wantedBird == null)
                return Task.FromResult<Bird?>(null!);

            return wantedBird;
        }
    }
}
