using Domain.Models;
using Infrastructure.Database;
using Infrastructure.RealDatabase;
using MediatR;

namespace Application.Commands.Dogs.DeleteDog
{
    public class DeleteDogByIdCommandHandler : IRequestHandler<DeleteDogByIdCommand, Dog>
    {
        private readonly MockDatabase _mockDatabase;
        private readonly MySqlDB _mySqlDB;

        public DeleteDogByIdCommandHandler(MockDatabase mockDatabase, MySqlDB mySqlDB)
        {
            _mockDatabase = mockDatabase;
            _mySqlDB = mySqlDB;
        }

        public Task<Dog> Handle(DeleteDogByIdCommand request, CancellationToken cancellationToken)
        {
            //Dog dogToDelete = _mockDatabase.Dogs.FirstOrDefault(dog => dog.Id == request.Id)!;
            Dog dogToDelete = _mySqlDB.Dogs.FirstOrDefault(dog => dog.Id == request.Id)!;

            if (dogToDelete == null)
                return Task.FromResult<Dog>(null!);

            //_mockDatabase.Dogs.Remove(dogToDelete);
            _mySqlDB.Dogs.Remove(dogToDelete);

            _mySqlDB.SaveChanges();

            return Task.FromResult(dogToDelete);
        }
    }
}
