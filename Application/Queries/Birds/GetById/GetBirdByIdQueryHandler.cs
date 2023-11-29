using Domain.Models;
using Infrastructure.Database;
using MediatR;

namespace Application.Queries.Birds
{
    public class GetBirdByIdQueryHandler : IRequestHandler<GetBirdByIdQuery, Bird>
    {
        private readonly MockDatabase _mockDatabase;

        public GetBirdByIdQueryHandler(MockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }

        public Task<Bird> Handle(GetBirdByIdQuery request, CancellationToken cancellationToken)
        {
            Bird wantedBird = _mockDatabase.Birds.FirstOrDefault(bird => bird.Id == request.Id)!;

            if (wantedBird == null)
                return Task.FromResult<Bird>(null!);

            return Task.FromResult(wantedBird);
        }
    }
}
