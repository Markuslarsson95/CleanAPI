using Application.Commands.Dogs;
using Application.Dtos;
using Infrastructure.Database;

namespace Test.DogTests.CommandTests
{
    [TestFixture]
    public class AddDogTests
    {
        private AddDogCommandHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            //Initialize the handler and mock database before each test
            _mockDatabase = new MockDatabase();
            _handler = new AddDogCommandHandler(_mockDatabase);
        }

        [Test]
        public async Task Handle_AddNewValidDog_ReturnsNewDogList()
        {
            // Arrange
            var addDogCommand = new AddDogCommand(new DogDto { Name = "testNameDog" });

            // Act
            var addedDog = await _handler.Handle(addDogCommand, CancellationToken.None);
            var allDogs = _mockDatabase.Dogs;

            // Assert
            Assert.NotNull(addedDog);
            Assert.Contains(addedDog, allDogs);
        }
    }
}
