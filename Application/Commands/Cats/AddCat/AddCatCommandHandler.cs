using Domain.Models;
using Domain.Repositories;
using MediatR;

namespace Application.Commands.Cats
{
    public class AddCatCommandHandler : IRequestHandler<AddCatCommand, Cat>
    {
        private readonly IGenericRepository<Cat> _catRepository;

        public AddCatCommandHandler(IGenericRepository<Cat> catRepository)
        {
            _catRepository = catRepository;
        }

        public Task<Cat> Handle(AddCatCommand request, CancellationToken cancellationToken)
        {
            Cat catToCreate = new()
            {
                Id = Guid.NewGuid(),
                Name = request.NewCat.Name,
                LikesToPlay = request.NewCat.LikesToPlay,
            };
            _catRepository.Add(catToCreate);

            _catRepository.Save();

            return Task.FromResult(catToCreate);
        }
    }
}
