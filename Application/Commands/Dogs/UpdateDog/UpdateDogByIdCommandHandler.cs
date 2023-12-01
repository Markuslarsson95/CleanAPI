using Domain.Models;
using Infrastructure.Database;
using Infrastructure.RealDatabase;
using MediatR;

namespace Application.Commands.Dogs.UpdateDog
{
    public class UpdateDogByIdCommandHandler : IRequestHandler<UpdateDogByIdCommand, Dog>
    {
        private readonly MockDatabase _mockDatabase;
        private readonly MySqlDB _mySqlDb;

        public UpdateDogByIdCommandHandler(MockDatabase mockDatabase, MySqlDB mySqlDb)
        {
            _mockDatabase = mockDatabase;
            _mySqlDb = mySqlDb;
        }
        public Task<Dog> Handle(UpdateDogByIdCommand request, CancellationToken cancellationToken)
        {
            //Dog dogToUpdate = _mockDatabase.Dogs.FirstOrDefault(dog => dog.Id == request.Id)!;
            Dog dogToUpdate = _mySqlDb.Dogs.FirstOrDefault(dog => dog.Id == request.Id)!;

            if (dogToUpdate == null)
                return Task.FromResult<Dog>(null!);

            dogToUpdate.Name = request.UpdatedDog.Name;

            _mySqlDb.SaveChanges();

            return Task.FromResult(dogToUpdate);
        }
    }
}
