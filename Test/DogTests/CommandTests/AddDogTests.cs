using Application.Commands.Dogs;
using Application.Dtos;
using Application.Queries.Dogs;
using Application.Queries.Dogs.GetAll;
using Infrastructure.Database;

namespace Test.DogTests.CommandTests
{
    [TestFixture]
    public class AddDogTests
    {
        private AddDogCommandHandler _handler;
        private GetAllDogsQueryHandler _allDogsHandler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            //Initialize the handler and mock database before each test
            _mockDatabase = new MockDatabase();
            _handler = new AddDogCommandHandler(_mockDatabase);
            _allDogsHandler = new GetAllDogsQueryHandler(_mockDatabase);
        }

        [Test]
        public async Task Handle_AddNewValidDog_ReturnsNewDog()
        {
            // Arrange
            var newDogDto = new DogDto { Name = "testNameDog" };
            var addDogCommand = new AddDogCommand(newDogDto);

            // Act
            var addedDog = await _handler.Handle(addDogCommand, CancellationToken.None);

            var getAllDogsQuery = new GetAllDogsQuery();
            var allDogs = await _allDogsHandler.Handle(getAllDogsQuery, CancellationToken.None);

            // Assert
            Assert.NotNull(addedDog);
            Assert.Contains(addedDog, allDogs);
        }
    }
}
