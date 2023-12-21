using Domain.Models.Animals;
using Infrastructure.Repositories.Cats;
using MediatR;
using Serilog;

namespace Application.Commands.Cats
{
    public class AddCatCommandHandler : IRequestHandler<AddCatCommand, Cat>
    {
        private readonly ICatRepository _catRepository;

        public AddCatCommandHandler(ICatRepository catRepository)
        {
            _catRepository = catRepository;
        }

        public async Task<Cat> Handle(AddCatCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Log.Information("Creating a new cat");

                Cat catToCreate = new()
                {
                    Id = Guid.NewGuid(),
                    Name = request.NewCat.Name,
                    LikesToPlay = request.NewCat.LikesToPlay,
                    Breed = request.NewCat.Breed,
                    Weight = request.NewCat.Weight,
                };

                await _catRepository.Add(catToCreate);

                return catToCreate;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while handling AddCatCommand");
                throw new Exception("Error occurred while handling AddCatCommand", ex);
            }
        }
    }
}
