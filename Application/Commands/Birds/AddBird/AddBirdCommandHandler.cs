using Domain.Models;
using Domain.Repositories;
using MediatR;

namespace Application.Commands.Birds
{
    public class AddBirdCommandHandler : IRequestHandler<AddBirdCommand, Bird>
    {
        private readonly IGenericRepository<Bird> _birdRepository;

        public AddBirdCommandHandler(IGenericRepository<Bird> birdRepository)
        {
            _birdRepository = birdRepository;
        }

        public async Task<Bird> Handle(AddBirdCommand request, CancellationToken cancellationToken)
        {
            Bird birdToCreate = new()
            {
                Id = Guid.NewGuid(),
                Name = request.NewBird.Name,
                CanFly = request.NewBird.CanFly
            };
            await _birdRepository.Add(birdToCreate);

            _birdRepository.Save();

            return birdToCreate;
        }
    }
}
