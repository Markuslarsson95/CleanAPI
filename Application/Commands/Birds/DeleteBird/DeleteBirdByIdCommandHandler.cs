using Domain.Models.Animals;
using Infrastructure.Repositories.Birds;
using MediatR;
using Serilog;

namespace Application.Commands.Birds
{
    public class DeleteBirdByIdCommandHandler : IRequestHandler<DeleteBirdByIdCommand, Bird>
    {
        private readonly IBirdRepository _birdRepository;

        public DeleteBirdByIdCommandHandler(IBirdRepository birdRepository)
        {
            _birdRepository = birdRepository;
        }

        public async Task<Bird> Handle(DeleteBirdByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Log.Information($"Deleting bird with ID {request.Id}");

                var birdToDelete = await _birdRepository.GetById(request.Id);

                if (birdToDelete == null)
                {
                    return await Task.FromResult<Bird>(null!);
                }

                await _birdRepository.Delete(birdToDelete);

                Log.Information("Successfully removed bird from the repository");

                return birdToDelete;
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"An error occurred while handling DeleteBirdByIdCommand for bird with ID {request.Id}");
                throw new Exception($"Error occurred while handling DeleteBirdByIdCommand for bird with ID {request.Id}", ex);
            }
        }
    }
}
