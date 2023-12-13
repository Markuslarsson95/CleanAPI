using Domain.Models.Animal;
using Infrastructure.Repositories;
using MediatR;

namespace Application.Commands.Cats
{
    public class DeleteCatByIdCommandHandler : IRequestHandler<DeleteCatByIdCommand, Cat>
    {
        private readonly ICatRepository _catRepository;

        public DeleteCatByIdCommandHandler(ICatRepository catRepository)
        {
            _catRepository = catRepository;
        }

        public async Task<Cat> Handle(DeleteCatByIdCommand request, CancellationToken cancellationToken)
        {
            var catToDelete = await _catRepository.GetById(request.Id);

            if (catToDelete == null)
                return await Task.FromResult<Cat>(null!);

            await _catRepository.Delete(catToDelete);

            return catToDelete;
        }
    }
}
