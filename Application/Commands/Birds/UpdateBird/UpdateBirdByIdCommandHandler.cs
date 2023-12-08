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
        public async Task<Bird> Handle(UpdateBirdByIdCommand request, CancellationToken cancellationToken)
        {
            var birdToUpdate = await _birdRepository.GetById(request.Id);

            if (birdToUpdate == null)
                return await Task.FromResult<Bird>(null!);

            birdToUpdate.Name = request.UpdatedBird.Name;
            birdToUpdate.CanFly = request.UpdatedBird.CanFly;
            await _birdRepository.Update(birdToUpdate);

            _birdRepository.Save();

            return birdToUpdate;
        }
    }
}
