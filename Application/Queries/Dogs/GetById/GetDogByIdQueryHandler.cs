using Domain.Models;
using Domain.Repositories;
using MediatR;

namespace Application.Queries.Dogs.GetById
{
    public class GetDogByIdQueryHandler : IRequestHandler<GetDogByIdQuery, Dog>
    {
        private readonly IGenericRepository<Dog> _dogRepository;

        public GetDogByIdQueryHandler(IGenericRepository<Dog> dogRepository)
        {
            _dogRepository = dogRepository;
        }

        public Task<Dog> Handle(GetDogByIdQuery request, CancellationToken cancellationToken)
        {
            var wantedDog = _dogRepository.GetById(request.Id);

            if (wantedDog == null)
                return Task.FromResult<Dog>(null!);

            return wantedDog;
        }
    }
}
