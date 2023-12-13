using Application.Queries.Cats.GetAll;
using Domain.Models.Animal;
using Infrastructure.Repositories;
using Moq;

namespace Test.catTests.QueryTest
{
    [TestFixture]
    public class GetAllCatsTests
    {
        private Mock<ICatRepository> _catRepositoryMock;
        private GetAllCatsQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            // Initialize the handler and mock database before each test
            _catRepositoryMock = new Mock<ICatRepository>();
            _handler = new GetAllCatsQueryHandler(_catRepositoryMock.Object);
        }

        [Test]
        public async Task Handle_Should_ReturncatList()
        {
            // Arrange
            var query = new GetAllCatsQuery();
            _catRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(new List<Cat>());

            // Act
            var result = await _handler.Handle(query, default);

            // Assert
            Assert.That(result, Is.Not.Null);
            _catRepositoryMock.Verify(x => x.GetAll(), Times.Once);
        }
    }
}
