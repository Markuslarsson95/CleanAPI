using Application.Queries.Birds.GetAll;
using Domain.Models.Animals;
using Infrastructure.Repositories;
using Moq;

namespace Test.BirdTests.QueryTest
{
    [TestFixture]
    public class GetAllBirdsTests
    {
        private Mock<IBirdRepository> _birdRepositoryMock;
        private GetAllBirdsQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _birdRepositoryMock = new Mock<IBirdRepository>();
            _handler = new GetAllBirdsQueryHandler(_birdRepositoryMock.Object);
        }

        [Test]
        public async Task Handle_Should_ReturnBirdList()
        {
            // Arrange
            var query = new GetAllBirdsQuery(sortyByColor: "Blue");
            var expectedBirds = new List<Bird>
            {
                new Bird { Id = Guid.NewGuid(), Color = "Blue", Name = "Bluebird" },
                new Bird { Id = Guid.NewGuid(), Color = "Blue", Name = "Blue Jay" }
            };

            _birdRepositoryMock.Setup(x => x.GetAll("Blue")).ReturnsAsync(expectedBirds);

            // Act
            var result = await _handler.Handle(query, default);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(expectedBirds));
            _birdRepositoryMock.Verify(x => x.GetAll("Blue"), Times.Once);
        }

        [Test]
        public async Task Handle_Should_ReturnEmptyBirdList_When_NoBirdWithCorrectColor()
        {
            // Arrange
            var query = new GetAllBirdsQuery(sortyByColor: "Yellow");
            var expectedBirds = new List<Bird>
            {

            };

            _birdRepositoryMock.Setup(x => x.GetAll("Yellow")).ReturnsAsync(expectedBirds);

            // Act
            var result = await _handler.Handle(query, default);

            // Assert
            Assert.That(result, Is.Empty);
            Assert.That(result, Is.EqualTo(expectedBirds));
            _birdRepositoryMock.Verify(x => x.GetAll("Yellow"), Times.Once);
        }
    }
}
