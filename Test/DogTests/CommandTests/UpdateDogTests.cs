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
            var updateDogCommand = new UpdateDogByIdCommand(new DogDto()
            {
                Name = "TestUpdateDog"
            }, new Guid("12345678-1234-5678-1234-867428755756"));

            // Act
            var updatedDog = await _handler.Handle(updateDogCommand, CancellationToken.None);
            var dogListAfterUpdate = await _allDogsHandler.Handle(new GetAllDogsQuery(), CancellationToken.None);

            // Assert
            Assert.NotNull(updatedDog);
            Assert.That(dogListAfterUpdate, Does.Contain(updatedDog));
        }

        [Test]
        public async Task Handle_UpdateDogInvalidId_ReturnsNull()
        {
            // Arrange
            var updateDogCommand = new UpdateDogByIdCommand(new DogDto()
            {
                Name = "TestUpdate"
            }, Guid.NewGuid());

            // Act
            var updateDog = await _handler.Handle(updateDogCommand, CancellationToken.None);

            // Assert
            Assert.IsNull(updateDog);
        }
    }
}
