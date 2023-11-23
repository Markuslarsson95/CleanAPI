using Application.Commands.Dogs.UpdateDog;
using Application.Dtos;
using Application.Queries.Dogs;
using Application.Queries.Dogs.GetAll;
using Infrastructure.Database;

namespace Test.DogTests.CommandTests
{
    [TestFixture]
    public class UpdateDogTests
    {
        private UpdateDogByIdCommandHandler _handler;
        private GetAllDogsQueryHandler _allDogsHandler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            //Initialize the handler and mock database before each test
            _mockDatabase = new MockDatabase();
            _handler = new UpdateDogByIdCommandHandler(_mockDatabase);
            _allDogsHandler = new GetAllDogsQueryHandler(_mockDatabase);
        }

        [Test]
        public async Task Handle_UpdateDogValidId_ReturnsUpdatedDogList()
        {
            // Arrange
            var updateDogId = new Guid("12345678-1234-5678-1234-867428755756");
            var updateDogDto = new DogDto()
            {
                Name = "TestUpdateDog"
            };

            var query = new GetAllDogsQuery();
            var updateDogCommand = new UpdateDogByIdCommand(updateDogDto, updateDogId);

            // Act
            var updatedDog = await _handler.Handle(updateDogCommand, CancellationToken.None);
            var dogListAfterUpdate = await _allDogsHandler.Handle(query, CancellationToken.None);


            // Assert
            Assert.NotNull(updatedDog);
            Assert.That(dogListAfterUpdate, Does.Contain(updatedDog));
        }

        [Test]
        public async Task Handle_UpdateDogInvalidId_ReturnsNullReferenceException()
        {
            // Arrange
            var invalidDogId = new Guid("87654321-4321-8765-4321-098765432109");
            var dogDto = new DogDto()
            {
                Name = "TestUpdate"
            };

            var command = new UpdateDogByIdCommand(dogDto, invalidDogId);

            // Act & Assert
            Assert.ThrowsAsync<NullReferenceException>(
                async () => await _handler.Handle(command, CancellationToken.None));
        }
    }
}
