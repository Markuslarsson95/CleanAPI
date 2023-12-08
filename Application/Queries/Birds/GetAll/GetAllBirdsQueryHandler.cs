using Domain.Models;
using Domain.Repositories;
using MediatR;

namespace Application.Queries.Birds.GetAll
{
    public class GetAllBirdsQueryHandler : IRequestHandler<GetAllBirdsQuery, List<Bird>>
    {
        private readonly IGenericRepository<Bird> _birdRepository;

        public GetAllBirdsQueryHandler(IGenericRepository<Bird> birdRepository)
        {
            _birdRepository = birdRepository;
        }
        public Task<List<Bird>> Handle(GetAllBirdsQuery request, CancellationToken cancellationToken)
        {
            var birdList = _birdRepository.GetAll();

            return birdList;
        }
    }
}
