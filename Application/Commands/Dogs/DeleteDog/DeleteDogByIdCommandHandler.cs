using Domain.Models.Animals;
using Infrastructure.Repositories.Dogs;
using MediatR;
using Serilog;

namespace Application.Commands.Dogs.DeleteDog
{
    public class DeleteDogByIdCommandHandler : IRequestHandler<DeleteDogByIdCommand, Dog>
    {
        private readonly IDogRepository _dogRepository;

        public DeleteDogByIdCommandHandler(IDogRepository dogRepository)
        {
            _dogRepository = dogRepository;
        }

        public async Task<Dog> Handle(DeleteDogByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var dogToDelete = await _dogRepository.GetById(request.Id);

                if (dogToDelete == null)
                {
                    Log.Warning($"Dog with ID {request.Id} not found during deletion");
                    return await Task.FromResult<Dog>(null!);
                }

                await _dogRepository.Delete(dogToDelete);

                Log.Information($"Successfully deleted dog with ID {request.Id}");

                return dogToDelete;
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"An error occurred while handling DeleteDogByIdCommand for dog with ID {request.Id}");
                throw new Exception(ex.Message);
            }
        }
    }
}
