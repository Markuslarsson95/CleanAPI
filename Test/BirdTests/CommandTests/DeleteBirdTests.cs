using Application.Commands.Birds;
using Infrastructure.Database;

namespace Test.BirdTests.CommandTests
{
    [TestFixture]
    public class DeleteBirdTests
    {
        private DeleteBirdByIdCommandHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            //Initialize the handler and mock database before each test
            _mockDatabase = new MockDatabase();
            _handler = new DeleteBirdByIdCommandHandler(_mockDatabase);
        }

        [Test]
        public async Task Handle_DeleteBirdValidId_RemovesBirdFromList()
        {
            // Arrange
            var deleteBirdCommand = new DeleteBirdByIdCommand(new Guid("12345678-1234-5678-1234-938538598395"));

            // Act
            var deletedBird = await _handler.Handle(deleteBirdCommand, CancellationToken.None);
            var birdListAfterDeletion = _mockDatabase.Birds;

            // Assert
            Assert.NotNull(deletedBird);
            Assert.That(birdListAfterDeletion, Does.Not.Contain(deletedBird));
        }

        [Test]
        public async Task Handle_DeleteBirdInvalidId_ReturnsNull()
        {
            // Arrange
            var deleteBirdCommand = new DeleteBirdByIdCommand(Guid.NewGuid());

            /// Act
            var deletedBird = await _handler.Handle(deleteBirdCommand, CancellationToken.None);

            // Assert
            Assert.Null(deletedBird);
        }
    }
}
