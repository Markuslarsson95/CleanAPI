using Domain.Models;
using Domain.Repositories;
using MediatR;

namespace Application.Commands.Cats
{
    public class DeleteCatByIdCommandHandler : IRequestHandler<DeleteCatByIdCommand, Cat>
    {
        private readonly IGenericRepository<Cat> _catRepository;

        public DeleteCatByIdCommandHandler(IGenericRepository<Cat> catRepository)
        {
            _catRepository = catRepository;
        }

        public Task<Cat> Handle(DeleteCatByIdCommand request, CancellationToken cancellationToken)
        {
            var catToDelete = _catRepository.GetById(request.Id);

            if (catToDelete == null)
                return Task.FromResult<Cat>(null!);

            _catRepository.Delete(catToDelete);

            _catRepository.Save();

            return Task.FromResult(catToDelete);
        }
    }
}
