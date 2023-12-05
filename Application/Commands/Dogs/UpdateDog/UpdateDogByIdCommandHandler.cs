using Domain.Models;
using Domain.Repositories;
using Infrastructure.RealDatabase;
using MediatR;

namespace Application.Commands.Dogs.UpdateDog
{
    public class UpdateDogByIdCommandHandler : IRequestHandler<UpdateDogByIdCommand, Dog>
    {
        private readonly MySqlDB _mySqlDB;
        private readonly IDogRepository _dogRepository;

        public UpdateDogByIdCommandHandler(MySqlDB mySqlDB, IDogRepository dogRepository)
        {
            _mySqlDB = mySqlDB;
            _dogRepository = dogRepository;
        }
        public async Task<Dog> Handle(UpdateDogByIdCommand request, CancellationToken cancellationToken)
        {
            //Dog dogToUpdate = _mockDatabase.Dogs.FirstOrDefault(dog => dog.Id == request.Id)!;
            var dogToUpdate = await _dogRepository.GetById(request.Id);

            if (dogToUpdate == null)
                return await Task.FromResult<Dog>(null!);

            dogToUpdate.Name = request.UpdatedDog.Name;
            _dogRepository.Update(dogToUpdate);

            _mySqlDB.SaveChanges();

            return await Task.FromResult(dogToUpdate);
        }
    }
}
