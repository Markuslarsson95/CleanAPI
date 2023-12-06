using Domain.Models;
using Domain.Repositories;
using MediatR;

namespace Application.Queries.Dogs.GetById
{
    public class GetDogByIdQueryHandler : IRequestHandler<GetDogByIdQuery, Dog>
    {
        private readonly IDogRepository _dogRepository;

        public GetDogByIdQueryHandler(IDogRepository dogRepository)
        {
            _dogRepository = dogRepository;
        }

        public async Task<Dog> Handle(GetDogByIdQuery request, CancellationToken cancellationToken)
        {
            var wantedDog = await _dogRepository.GetById(request.Id);

            if (wantedDog == null)
                return Task.FromResult<Dog>(null!).Result;

            return Task.FromResult(wantedDog).Result;
        }
    }
}
