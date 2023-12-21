using Application.Queries.Cats.GetById;
using Domain.Models.Animals;
using Infrastructure.Repositories.Cats;
using Moq;

namespace Test.CatTests.QueryTest
{
    [TestFixture]
    public class GetCatByIdTests
    {
        private Mock<ICatRepository> _catRepositoryMock;
        private GetCatByIdQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            // Initialize the handler and mock database before each test
            _catRepositoryMock = new Mock<ICatRepository>();
            _handler = new GetCatByIdQueryHandler(_catRepositoryMock.Object);
        }

        [Test]
        public async Task Handle_Should_ReturnCat_WhenValidId()
        {
            // Arrange
            var query = new GetCatByIdQuery(Guid.NewGuid());

            _catRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(new Cat { Id = Guid.NewGuid(), Name = "Cat", LikesToPlay = true });

            // Act
            var result = await _handler.Handle(query, default);

            // Assert
            Assert.That(result, Is.Not.Null);
            _catRepositoryMock.Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        public async Task Handle_Should_ReturnNull_WhenNotValidId()
        {
            // Arrange
            var query = new GetCatByIdQuery(Guid.NewGuid());

            // Setup the mock to return null
            _catRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync((Cat)null!);

            // Act
            var result = await _handler.Handle(query, default);

            // Assert
            Assert.That(result, Is.Null);
            _catRepositoryMock.Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);
        }
    }
}
