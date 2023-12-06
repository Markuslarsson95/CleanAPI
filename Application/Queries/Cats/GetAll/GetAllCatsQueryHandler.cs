using Domain.Models;
using Domain.Repositories;
using MediatR;

namespace Application.Queries.Cats.GetAll
{
    public class GetAllCatsQueryHandler : IRequestHandler<GetAllCatsQuery, IEnumerable<Cat>>
    {
        private readonly IGenericRepository<Cat> _catRepository;

        public GetAllCatsQueryHandler(IGenericRepository<Cat> catRepository)
        {
            _catRepository = catRepository;
        }
        public Task<IEnumerable<Cat>> Handle(GetAllCatsQuery request, CancellationToken cancellationToken)
        {
            var catList = _catRepository.GetAll();

            return Task.FromResult(catList);
        }
    }
}
