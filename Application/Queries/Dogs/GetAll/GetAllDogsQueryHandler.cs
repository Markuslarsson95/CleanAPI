using Application.Queries.Dogs.GetAll;
using Domain.Models;
using Domain.Repositories;
using MediatR;

namespace Application.Queries.Dogs
{
    public class GetAllDogsQueryHandler : IRequestHandler<GetAllDogsQuery, IEnumerable<Dog>>
    {
        private readonly IGenericRepository<Dog> _dogRepository;

        public GetAllDogsQueryHandler(IGenericRepository<Dog> dogRepository)
        {
            _dogRepository = dogRepository;
        }
        public Task<IEnumerable<Dog>> Handle(GetAllDogsQuery request, CancellationToken cancellationToken)
        {
            var dogList = _dogRepository.GetAll();

            return Task.FromResult(dogList);
        }
    }
}
