using Domain.Models;
using Infrastructure.Repositories;
using MediatR;

namespace Application.Queries.Cats.GetById
{
    public class GetCatByIdQueryHandler : IRequestHandler<GetCatByIdQuery, Cat?>
    {
        private readonly ICatRepository _catRepository;

        public GetCatByIdQueryHandler(ICatRepository catRepository)
        {
            _catRepository = catRepository;
        }

        public Task<Cat?> Handle(GetCatByIdQuery request, CancellationToken cancellationToken)
        {
            var wantedCat = _catRepository.GetById(request.Id);

            if (wantedCat == null)
                return Task.FromResult<Cat?>(null!);

            return wantedCat;
        }
    }
}
