using Application.Queries.Dogs.GetAll;
using Domain.Models;
using Domain.Repositories;
using MediatR;

namespace Application.Queries.Dogs
{
    public class GetAllDogsQueryHandler : IRequestHandler<GetAllDogsQuery, List<Dog>>
    {
        private readonly IDogRepository _dogRepository;

        public GetAllDogsQueryHandler(IDogRepository dogRepository)
        {
            _dogRepository = dogRepository;
        }
        public async Task<List<Dog>> Handle(GetAllDogsQuery request, CancellationToken cancellationToken)
        {
            var dogList = await _dogRepository.GetAll();

            return Task.FromResult(dogList).Result;
        }
    }
}
