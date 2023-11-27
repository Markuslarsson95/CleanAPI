using Application.Commands.Birds;
using Application.Dtos;
using Infrastructure.Database;

namespace Test.BirdTests.CommandTests
{
    [TestFixture]
    public class UpdateBirdTests
    {
        private UpdateBirdByIdCommandHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            //Initialize the handler and mock database before each test
            _mockDatabase = new MockDatabase();
            _handler = new UpdateBirdByIdCommandHandler(_mockDatabase);
        }

        [Test]
        public async Task Handle_UpdateBirdValidId_ReturnsUpdatedBirdList()
        {
            // Arrange
            var updateBirdComand = new UpdateBirdByIdCommand(new BirdDto()
            {
                Name = "TestUpdateBird",
                CanFly = true
            }, new Guid("12345678-1234-5678-1234-467580398558"));

            // Act
            var updatedBird = await _handler.Handle(updateBirdComand, CancellationToken.None);
            var birdListAfterUpdate = _mockDatabase.Birds;

            // Assert
            Assert.NotNull(updatedBird);
            Assert.That(birdListAfterUpdate, Does.Contain(updatedBird));
        }

        [Test]
        public async Task Handle_UpdateBirdInvalidId_ReturnsNullReferenceException()
        {
            // Arrange
            var updateBirdCommand = new UpdateBirdByIdCommand(new BirdDto()
            {
                Name = "TestUpdateBird",
                CanFly = true
            }, Guid.NewGuid());

            // Act
            var updatedBird = await _handler.Handle(updateBirdCommand, CancellationToken.None);

            // Assert
            Assert.IsNull(updatedBird);
        }
    }
}
