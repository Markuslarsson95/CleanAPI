using Domain.Models;
using Domain.Repositories;
using MediatR;

namespace Application.Commands.Birds
{
    public class UpdateBirdByIdCommandHandler : IRequestHandler<UpdateBirdByIdCommand, Bird>
    {
        private readonly IGenericRepository<Bird> _birdRepository;

        public UpdateBirdByIdCommandHandler(IGenericRepository<Bird> birdRepository)
        {
            _birdRepository = birdRepository;
        }
        public Task<Bird> Handle(UpdateBirdByIdCommand request, CancellationToken cancellationToken)
        {
            var birdToUpdate = _birdRepository.GetById(request.Id);

            if (birdToUpdate == null)
                return Task.FromResult<Bird>(null!);

            birdToUpdate.Name = request.UpdatedBird.Name;
            birdToUpdate.CanFly = request.UpdatedBird.CanFly;
            _birdRepository.Update(birdToUpdate);

            _birdRepository.Save();

            return Task.FromResult(birdToUpdate);
        }
    }
}
