using Domain.Models;
using Domain.Repositories;
using MediatR;

namespace Application.Commands.Dogs.UpdateDog
{
    public class UpdateDogByIdCommandHandler : IRequestHandler<UpdateDogByIdCommand, Dog>
    {
        private readonly IGenericRepository<Dog> _dogRepository;

        public UpdateDogByIdCommandHandler(IGenericRepository<Dog> dogRepository)
        {
            _dogRepository = dogRepository;
        }
        public async Task<Dog> Handle(UpdateDogByIdCommand request, CancellationToken cancellationToken)
        {
            var dogToUpdate = await _dogRepository.GetById(request.Id);

            if (dogToUpdate == null)
                return await Task.FromResult<Dog>(null!);

            dogToUpdate.Name = request.UpdatedDog.Name;
            await _dogRepository.Update(dogToUpdate);

            _dogRepository.Save();

            return dogToUpdate;
        }
    }
}
