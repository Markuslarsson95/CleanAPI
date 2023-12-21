using Domain.Models.Animals;
using Infrastructure.Repositories.Dogs;
using MediatR;
using Serilog;

namespace Application.Commands.Dogs
{
    public class AddDogCommandHandler : IRequestHandler<AddDogCommand, Dog>
    {
        private readonly IDogRepository _dogRepository;

        public AddDogCommandHandler(IDogRepository dogRepository)
        {
            _dogRepository = dogRepository;
        }

        public async Task<Dog> Handle(AddDogCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Log.Information("Creating a new dog");

                Dog dogToCreate = new()
                {
                    Id = Guid.NewGuid(),
                    Name = request.NewDog.Name,
                    Breed = request.NewDog.Breed,
                    Weight = request.NewDog.Weight,
                };

                await _dogRepository.Add(dogToCreate);

                Log.Information("Successfully created a new dog");

                return dogToCreate;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while handling AddDogCommand");
                throw new Exception(ex.Message);
            }
        }
    }
}
