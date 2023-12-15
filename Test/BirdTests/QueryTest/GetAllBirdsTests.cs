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
            // Initialize the handler and mock database before each test
            _birdRepositoryMock = new Mock<IBirdRepository>();
            _handler = new GetAllBirdsQueryHandler(_birdRepositoryMock.Object);
        }

        [Test]
        public async Task Handle_Should_ReturnBirdList()
        {
            // Arrange
            var query = new GetAllBirdsQuery();
            _birdRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(new List<Bird>());

            // Act
            var result = await _handler.Handle(query, default);

            // Assert
            Assert.That(result, Is.Not.Null);
            _birdRepositoryMock.Verify(x => x.GetAll(), Times.Once);
        }
    }
}
