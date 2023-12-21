using Domain.Models.Animals;
using Infrastructure.Repositories.Birds;
using MediatR;
using Serilog;

namespace Application.Commands.Birds
{
    public class UpdateBirdByIdCommandHandler : IRequestHandler<UpdateBirdByIdCommand, Bird>
    {
        private readonly IBirdRepository _birdRepository;

        public UpdateBirdByIdCommandHandler(IBirdRepository birdRepository)
        {
            _birdRepository = birdRepository;
        }
        public async Task<Bird> Handle(UpdateBirdByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Log.Information($"Updating bird with ID {request.Id}");

                var birdToUpdate = await _birdRepository.GetById(request.Id);

                if (birdToUpdate == null)
                {
                    return await Task.FromResult<Bird>(null!);
                }

                birdToUpdate.Name = request.UpdatedBird.Name;
                birdToUpdate.CanFly = request.UpdatedBird.CanFly;
                birdToUpdate.Color = request.UpdatedBird.Color;

                await _birdRepository.Update(birdToUpdate);

                Log.Information("Successfully updated bird in the repository");

                return birdToUpdate;
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"An error occurred while handling UpdateBirdByIdCommand for bird with ID {request.Id}");
                throw new Exception($"Error occurred while handling UpdateBirdByIdCommand for bird with ID {request.Id}", ex);
            }
        }
    }
}
