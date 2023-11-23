using Application.Commands.Birds;
using Application.Dtos;
using Application.Queries.Birds.GetAll;
using Infrastructure.Database;

namespace Test.BirdTests.CommandTests
{
    [TestFixture]
    public class UpdateBirdTests
    {
        private UpdateBirdByIdCommandHandler _handler;
        private GetAllBirdsQueryHandler _allBirdsHandler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            //Initialize the handler and mock database before each test
            _mockDatabase = new MockDatabase();
            _handler = new UpdateBirdByIdCommandHandler(_mockDatabase);
            _allBirdsHandler = new GetAllBirdsQueryHandler(_mockDatabase);
        }

        [Test]
        public async Task Handle_UpdateBirdValidId_ReturnsUpdatedBirdList()
        {
            // Arrange
            var birdId = new Guid("12345678-1234-5678-1234-467580398558");
            var updateBirdDto = new BirdDto()
            {
                Name = "TestUpdateBird",
                CanFly = true
            };

            var query = new GetAllBirdsQuery();
            var updateBirdComand = new UpdateBirdByIdCommand(updateBirdDto, birdId);

            // Act
            var updatedBird = await _handler.Handle(updateBirdComand, CancellationToken.None);
            var birdListAfterUpdate = await _allBirdsHandler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(updatedBird);
            Assert.That(birdListAfterUpdate, Does.Contain(updatedBird));
        }

        [Test]
        public async Task Handle_UpdateBirdInvalidId_ReturnsNullReferenceException()
        {
            // Arrange
            var invalidBirdId = Guid.NewGuid();
            var updateBirdDto = new BirdDto()
            {
                Name = "TestUpdateBird",
                CanFly = true
            };

            var updateBirdCommand = new UpdateBirdByIdCommand(updateBirdDto, invalidBirdId);

            // Act & Assert
            Assert.ThrowsAsync<NullReferenceException>(
                async () => await _handler.Handle(updateBirdCommand, CancellationToken.None));
        }
    }
}
