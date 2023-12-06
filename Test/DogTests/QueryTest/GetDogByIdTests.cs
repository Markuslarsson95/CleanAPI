using Application.Queries.Dogs.GetById;
using Domain.Models;
using Domain.Repositories;
using Moq;

namespace Test.DogTests.QueryTest
{
    [TestFixture]
    public class GetDogByIdTests
    {
        private Mock<IDogRepository> _dogRepositoryMock;
        private GetDogByIdQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            // Initialize the handler and mock database before each test
            _dogRepositoryMock = new Mock<IDogRepository>();
            _handler = new GetDogByIdQueryHandler(_dogRepositoryMock.Object);
        }

        [Test]
        public async Task Handle_Should_ReturnDog_WhenValidId()
        {
            // Arrange
            var query = new GetDogByIdQuery(Guid.NewGuid());

            _dogRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(new Dog { Id = Guid.NewGuid(), Name = "Update" });

            // Act
            var result = await _handler.Handle(query, default);

            // Assert
            Assert.That(result, Is.Not.Null);
            _dogRepositoryMock.Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        public async Task Handle_Should_ReturnNull_WhenNotValidId()
        {
            // Arrange
            var query = new GetDogByIdQuery(Guid.NewGuid());

            // Setup the mock to return null
            _dogRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync((Dog)null!);

            // Act
            var result = await _handler.Handle(query, default);

            // Assert
            Assert.That(result, Is.Null);
            _dogRepositoryMock.Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);
        }
    }
}
