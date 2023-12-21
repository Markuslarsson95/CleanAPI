using Domain.Models.Animals;
using Infrastructure.Repositories.Dogs;
using MediatR;
using Serilog;

namespace Application.Commands.Dogs.UpdateDog
{
    public class UpdateDogByIdCommandHandler : IRequestHandler<UpdateDogByIdCommand, Dog>
    {
        private readonly IDogRepository _dogRepository;

        public UpdateDogByIdCommandHandler(IDogRepository dogRepository)
        {
            _dogRepository = dogRepository;
        }
        public async Task<Dog> Handle(UpdateDogByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var dogToUpdate = await _dogRepository.GetById(request.Id);

                if (dogToUpdate == null)
                {
                    Log.Warning($"Dog with ID {request.Id} not found during update");
                    return await Task.FromResult<Dog>(null!);
                }

                dogToUpdate.Name = request.UpdatedDog.Name;
                dogToUpdate.Weight = request.UpdatedDog.Weight;
                dogToUpdate.Breed = request.UpdatedDog.Breed;

                await _dogRepository.Update(dogToUpdate);

                Log.Information($"Sending updated dog with ID {request.Id} to the API");

                return dogToUpdate;
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"An error occurred while handling UpdateDogByIdCommand for dog with ID {request.Id}");
                throw new Exception(ex.Message);
            }
        }
    }
}
