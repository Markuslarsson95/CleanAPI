using Application.Queries.Dogs;
using Application.Queries.Dogs.GetAll;
using Domain.Models.Animals;
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
            _dogRepositoryMock = new Mock<IDogRepository>();
            _handler = new GetAllDogsQueryHandler(_dogRepositoryMock.Object);
        }

        [Test]
        public async Task Handle_Should_ReturnDogList()
        {
            // Arrange
            var query = new GetAllDogsQuery(sortyByBreed: "Bulldog", 25);
            var expectedDogs = new List<Dog>
            {
                new Dog { Id = Guid.NewGuid(), Breed = "Bulldog", Weight = 30, Name = "Boss" },
                new Dog { Id = Guid.NewGuid(), Breed = "Bulldog", Weight = 25, Name = "Pluto" }
            };
            _dogRepositoryMock.Setup(x => x.GetAll("Bulldog", 25)).ReturnsAsync(expectedDogs);

            // Act
            var result = await _handler.Handle(query, default);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(expectedDogs));
            _dogRepositoryMock.Verify(x => x.GetAll("Bulldog", 25), Times.Once);
        }

        [Test]
        public async Task Handle_Should_ReturnEmptyDogList_When_NoDogWithCorrectWeight()
        {
            // Arrange
            var query = new GetAllDogsQuery(sortyByBreed: "Bulldog", 10);
            var expectedDogs = new List<Dog>
            {

            };

            _dogRepositoryMock.Setup(x => x.GetAll("Bulldog", 10)).ReturnsAsync(expectedDogs);

            // Act
            var result = await _handler.Handle(query, default);

            // Assert
            Assert.That(result, Is.Empty);
            Assert.That(result, Is.EqualTo(expectedDogs));
            _dogRepositoryMock.Verify(x => x.GetAll("Bulldog", 10), Times.Once);
        }
    }
}
