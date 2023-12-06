using Domain.Models;
using Domain.Repositories;
using MediatR;

namespace Application.Queries.Birds
{
    public class GetBirdByIdQueryHandler : IRequestHandler<GetBirdByIdQuery, Bird>
    {
        private readonly IGenericRepository<Bird> _birdRepository;

        public GetBirdByIdQueryHandler(IGenericRepository<Bird> birdRepository)
        {
            _birdRepository = birdRepository;
        }

        public Task<Bird> Handle(GetBirdByIdQuery request, CancellationToken cancellationToken)
        {
            var wantedBird = _birdRepository.GetById(request.Id);

            if (wantedBird == null)
                return Task.FromResult<Bird>(null!);

            return Task.FromResult(wantedBird);
        }
    }
}
