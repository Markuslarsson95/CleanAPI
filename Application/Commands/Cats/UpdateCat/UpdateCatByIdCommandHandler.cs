using Domain.Models;
using Domain.Repositories;
using MediatR;

namespace Application.Commands.Cats
{
    public class UpdateCatByIdCommandHandler : IRequestHandler<UpdateCatByIdCommand, Cat>
    {
        private readonly IGenericRepository<Cat> _catRepository;

        public UpdateCatByIdCommandHandler(IGenericRepository<Cat> catRepository)
        {
            _catRepository = catRepository;
        }
        public async Task<Cat> Handle(UpdateCatByIdCommand request, CancellationToken cancellationToken)
        {
            var catToUpdate = await _catRepository.GetById(request.Id);

            if (catToUpdate == null)
                return await Task.FromResult<Cat>(null!);

            catToUpdate.Name = request.UpdatedCat.Name;
            catToUpdate.LikesToPlay = request.UpdatedCat.LikesToPlay;
            await _catRepository.Update(catToUpdate);

            _catRepository.Save();

            return catToUpdate;
        }
    }
}
