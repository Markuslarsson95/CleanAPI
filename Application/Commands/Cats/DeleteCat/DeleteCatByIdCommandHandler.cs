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

        public async Task<Cat> Handle(DeleteCatByIdCommand request, CancellationToken cancellationToken)
        {
            var catToDelete = await _catRepository.GetById(request.Id);

            if (catToDelete == null)
                return await Task.FromResult<Cat>(null!);

            await _catRepository.Delete(catToDelete);

            _catRepository.Save();

            return catToDelete;
        }
    }
}
