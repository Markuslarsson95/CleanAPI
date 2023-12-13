using Domain.Models.Animal;
using Infrastructure.Repositories;
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

        public Task<Dog> Handle(GetDogByIdQuery request, CancellationToken cancellationToken)
        {
            var wantedDog = _dogRepository.GetById(request.Id);

            if (wantedDog == null)
                return Task.FromResult<Dog>(null!);

            return wantedDog;
        }
    }
}
