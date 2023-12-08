using Domain.Models;
using Domain.Repositories;
using MediatR;

namespace Application.Commands.Dogs
{
    public class AddDogCommandHandler : IRequestHandler<AddDogCommand, Dog>
    {
        private readonly IGenericRepository<Dog> _dogRepository;

        public AddDogCommandHandler(IGenericRepository<Dog> dogRepository)
        {
            _dogRepository = dogRepository;
        }

        public async Task<Dog> Handle(AddDogCommand request, CancellationToken cancellationToken)
        {
            Dog dogToCreate = new()
            {
                Id = Guid.NewGuid(),
                Name = request.NewDog.Name
            };
            await _dogRepository.Add(dogToCreate);

            _dogRepository.Save();

            return dogToCreate;
        }
    }
}
