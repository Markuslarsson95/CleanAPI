using Application.Queries.Birds;
using Infrastructure.Database;

namespace Test.BirdTests.QueryTest
{
    [TestFixture]
    public class GetBirdByIdTests
    {
        private GetBirdByIdQueryHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            // Initialize the handler and mock database before each test
            _mockDatabase = new MockDatabase();
            _handler = new GetBirdByIdQueryHandler(_mockDatabase);
        }

        [Test]
        public async Task Handle_ValidId_ReturnsCorrectBird()
        {
            // Arrange
            var birdId = new Guid("12345678-1234-5678-1234-746573875749");

            // Act
            var bird = await _handler.Handle(new GetBirdByIdQuery(birdId), CancellationToken.None);

            // Assert
            Assert.NotNull(bird);
            Assert.That(bird.Id, Is.EqualTo(birdId));
        }

        [Test]
        public async Task Handle_InvalidId_ReturnsNull()
        {
            // Arrange
            var invalidBirdId = Guid.NewGuid();

            // Act
            var bird = await _handler.Handle(new GetBirdByIdQuery(invalidBirdId), CancellationToken.None);

            // Assert
            Assert.IsNull(bird);
        }
    }
}
