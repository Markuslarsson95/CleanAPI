using Domain.Models;
using Domain.Repositories;
using Infrastructure.RealDatabase;
using MediatR;

namespace Application.Commands.Dogs.DeleteDog
{
    public class DeleteDogByIdCommandHandler : IRequestHandler<DeleteDogByIdCommand, Dog>
    {
        private readonly MySqlDB _mySqlDB;
        private readonly IDogRepository _dogRepository;

        public DeleteDogByIdCommandHandler(MySqlDB mySqlDB, IDogRepository dogRepository)
        {
            _mySqlDB = mySqlDB;
            _dogRepository = dogRepository;
        }

        public async Task<Dog> Handle(DeleteDogByIdCommand request, CancellationToken cancellationToken)
        {
            //Dog dogToDelete = _mockDatabase.Dogs.FirstOrDefault(dog => dog.Id == request.Id)!;
            var dogToDelete = await _dogRepository.GetById(request.Id);

            if (dogToDelete == null)
                return await Task.FromResult<Dog>(null!);

            _dogRepository.Delete(dogToDelete);
            await _mySqlDB.SaveChangesAsync(cancellationToken);

            return Task.FromResult(dogToDelete).Result;
        }
    }
}
