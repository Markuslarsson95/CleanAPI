using Application.Queries.Birds.GetAll;
using Domain.Models;
using Infrastructure.Database;

namespace Test.BirdTests.QueryTest
{
    [TestFixture]
    public class GetAllBirdsTests
    {
        private GetAllBirdsQueryHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            // Initialize the handler and mock database before each test
            _mockDatabase = new MockDatabase();
            _handler = new GetAllBirdsQueryHandler(_mockDatabase);
        }

        [Test]
        public async Task Handle_ValidList_ReturnsAllBirds()
        {
            // Arrange
            List<Bird> birds = _mockDatabase.Birds;

            // Act
            var birdList = await _handler.Handle(new GetAllBirdsQuery(), CancellationToken.None);

            // Assert
            Assert.NotNull(birdList);
            Assert.That(birdList, Is.EqualTo(birds));
        }
    }
}
