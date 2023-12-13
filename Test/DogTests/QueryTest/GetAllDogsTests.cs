using Application.Queries.Dogs;
using Application.Queries.Dogs.GetAll;
using Domain.Models.Animal;
using Infrastructure.Repositories;
using Moq;

namespace Test.DogTests.QueryTest
{
    [TestFixture]
    public class GetAllDogsTests
    {
        private Mock<IDogRepository> _dogRepositoryMock;
        private GetAllDogsQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            // Initialize the handler and mock database before each test
            _dogRepositoryMock = new Mock<IDogRepository>();
            _handler = new GetAllDogsQueryHandler(_dogRepositoryMock.Object);
        }

        [Test]
        public async Task Handle_Should_ReturnDogList()
        {
            // Arrange
            var query = new GetAllDogsQuery();
            _dogRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(new List<Dog>());

            // Act
            var result = await _handler.Handle(query, default);

            // Assert
            Assert.That(result, Is.Not.Null);
            _dogRepositoryMock.Verify(x => x.GetAll(), Times.Once);
        }
    }
}
