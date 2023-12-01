using Domain.Models;
using Infrastructure.Database;
using Infrastructure.RealDatabase;
using MediatR;

namespace Application.Commands.Dogs
{
    public class AddDogCommandHandler : IRequestHandler<AddDogCommand, Dog>
    {
        private readonly MockDatabase _mockDatabase;
        private readonly MySqlDB _mySqlDb;

        public AddDogCommandHandler(MockDatabase mockDatabase, MySqlDB mySqlDb)
        {
            _mockDatabase = mockDatabase;
            _mySqlDb = mySqlDb;
        }

        public Task<Dog> Handle(AddDogCommand request, CancellationToken cancellationToken)
        {
            Dog dogToCreate = new()
            {
                Id = Guid.NewGuid(),
                Name = request.NewDog.Name
            };

            //_mockDatabase.Dogs.Add(dogToCreate);
            _mySqlDb.Dogs.Add(dogToCreate);

            _mySqlDb.SaveChanges();

            return Task.FromResult(dogToCreate);
        }
    }
}
