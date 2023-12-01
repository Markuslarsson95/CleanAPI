using Domain.Models;
using Infrastructure.Database;
using Infrastructure.RealDatabase;
using MediatR;

namespace Application.Queries.Dogs.GetById
{
    public class GetDogByIdQueryHandler : IRequestHandler<GetDogByIdQuery, Dog>
    {
        private readonly MockDatabase _mockDatabase;
        private readonly MySqlDB _mySqlDb;

        public GetDogByIdQueryHandler(MockDatabase mockDatabase, MySqlDB mySqlDb)
        {
            _mockDatabase = mockDatabase;
            _mySqlDb = mySqlDb;
        }

        public Task<Dog> Handle(GetDogByIdQuery request, CancellationToken cancellationToken)
        {
            //Dog wantedDog = _mockDatabase.Dogs.FirstOrDefault(dog => dog.Id == request.Id)!;
            Dog wantedDog = _mySqlDb.Dogs.FirstOrDefault(dog => dog.Id == request.Id)!;

            if (wantedDog == null)
                return Task.FromResult<Dog>(null!);

            return Task.FromResult(wantedDog);
        }
    }
}
