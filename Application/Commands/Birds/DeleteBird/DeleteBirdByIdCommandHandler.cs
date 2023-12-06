using Domain.Models;
using Domain.Repositories;
using MediatR;

namespace Application.Commands.Birds
{
    public class DeleteBirdByIdCommandHandler : IRequestHandler<DeleteBirdByIdCommand, Bird>
    {
        private readonly IGenericRepository<Bird> _birdRepository;

        public DeleteBirdByIdCommandHandler(IGenericRepository<Bird> birdRepository)
        {
            _birdRepository = birdRepository;
        }

        public Task<Bird> Handle(DeleteBirdByIdCommand request, CancellationToken cancellationToken)
        {
            var birdToDelete = _birdRepository.GetById(request.Id);

            if (birdToDelete == null)
                return Task.FromResult<Bird>(null!);

            _birdRepository.Delete(birdToDelete);

            _birdRepository.Save();

            return Task.FromResult(birdToDelete);
        }
    }
}
