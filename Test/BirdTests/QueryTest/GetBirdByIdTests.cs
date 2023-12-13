using Application.Queries.Birds;
using Domain.Models.Animal;
using Infrastructure.Repositories;
using Moq;

namespace Test.BirdTests.QueryTest
{
    [TestFixture]
    public class GetBirdByIdTests
    {
        private Mock<IBirdRepository> _birdRepositoryMock;
        private GetBirdByIdQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            // Initialize the handler and mock database before each test
            _birdRepositoryMock = new Mock<IBirdRepository>();
            _handler = new GetBirdByIdQueryHandler(_birdRepositoryMock.Object);
        }

        [Test]
        public async Task Handle_Should_ReturnBird_WhenValidId()
        {
            // Arrange
            var query = new GetBirdByIdQuery(Guid.NewGuid());

            _birdRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(new Bird { Id = Guid.NewGuid(), Name = "Update", CanFly = false });

            // Act
            var result = await _handler.Handle(query, default);

            // Assert
            Assert.That(result, Is.Not.Null);
            _birdRepositoryMock.Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        public async Task Handle_Should_ReturnNull_WhenNotValidId()
        {
            // Arrange
            var query = new GetBirdByIdQuery(Guid.NewGuid());

            // Setup the mock to return null
            _birdRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync((Bird)null!);

            // Act
            var result = await _handler.Handle(query, default);

            // Assert
            Assert.That(result, Is.Null);
            _birdRepositoryMock.Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);
        }
    }
}
