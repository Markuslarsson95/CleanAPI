using Domain.Models.Animals;
using Infrastructure.Repositories.Cats;
using MediatR;
using Serilog;

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
            try
            {
                Log.Information("GetById in catRepository called");
                var wantedCat = _catRepository.GetById(request.Id);

                return wantedCat;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An unexpected error occurred.");
                throw new Exception(ex.Message);
            }
        }
    }
}
