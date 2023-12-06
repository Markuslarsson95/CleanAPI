using Domain.Models;
using Domain.Repositories;
using MediatR;

namespace Application.Commands.Dogs.DeleteDog
{
    public class DeleteDogByIdCommandHandler : IRequestHandler<DeleteDogByIdCommand, Dog>
    {
        private readonly IGenericRepository<Dog> _dogRepository;

        public DeleteDogByIdCommandHandler(IGenericRepository<Dog> dogRepository)
        {
            _dogRepository = dogRepository;
        }

        public Task<Dog> Handle(DeleteDogByIdCommand request, CancellationToken cancellationToken)
        {
            var dogToDelete = _dogRepository.GetById(request.Id);

            if (dogToDelete == null)
                return Task.FromResult<Dog>(null!);

            _dogRepository.Delete(dogToDelete);

            _dogRepository.Save();

            return Task.FromResult(dogToDelete);
        }
    }
}
