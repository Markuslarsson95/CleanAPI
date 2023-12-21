using Domain.Models.Animals;
using Infrastructure.Repositories.Birds;
using MediatR;
using Serilog;

namespace Application.Commands.Birds
{
    public class AddBirdCommandHandler : IRequestHandler<AddBirdCommand, Bird>
    {
        private readonly IBirdRepository _birdRepository;

        public AddBirdCommandHandler(IBirdRepository birdRepository)
        {
            _birdRepository = birdRepository;
        }

        public async Task<Bird> Handle(AddBirdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Log.Information("Adding a new bird to the repository");

                Bird birdToCreate = new()
                {
                    Id = Guid.NewGuid(),
                    Name = request.NewBird.Name,
                    CanFly = request.NewBird.CanFly,
                    Color = request.NewBird.Color
                };

                await _birdRepository.Add(birdToCreate);

                Log.Information("Successfully added a new bird to the repository");

                return birdToCreate;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while handling AddBirdCommand");
                throw new Exception("Error occurred while handling AddBirdCommand", ex);
            }
        }
    }
}
