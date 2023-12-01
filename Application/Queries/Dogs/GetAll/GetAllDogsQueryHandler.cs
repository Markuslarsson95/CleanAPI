using Application.Queries.Dogs.GetAll;
using Domain.Models;
using Infrastructure.Database;
using Infrastructure.RealDatabase;
using MediatR;

namespace Application.Queries.Dogs
{
    public class GetAllDogsQueryHandler : IRequestHandler<GetAllDogsQuery, List<Dog>>
    {
        private readonly MockDatabase _mockDatabase;
        private readonly MySqlDB _mySqlDb;

        public GetAllDogsQueryHandler(MockDatabase mockDatabase, MySqlDB mySqlDb)
        {
            _mockDatabase = mockDatabase;
            _mySqlDb = mySqlDb;
        }
        public Task<List<Dog>> Handle(GetAllDogsQuery request, CancellationToken cancellationToken)
        {
            //List<Dog> allDogsFromMockDatabase = _mockDatabase.Dogs;
            List<Dog> allDogsFromMockDatabase = _mySqlDb.Dogs.ToList();
            return Task.FromResult(allDogsFromMockDatabase);
        }
    }
}
