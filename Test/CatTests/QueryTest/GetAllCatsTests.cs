using Application.Queries.Cats.GetAll;
using Domain.Models.Animals;
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
            _catRepositoryMock = new Mock<ICatRepository>();
            _handler = new GetAllCatsQueryHandler(_catRepositoryMock.Object);
        }

        [Test]
        public async Task Handle_Should_ReturncatList()
        {
            // Arrange
            var query = new GetAllCatsQuery(sortyByBreed: "Ragdoll", sortByWeight: 10);
            var expectedCats = new List<Cat>
            {
                new Cat { Id = Guid.NewGuid(), Breed = "Ragdoll", Weight = 10, Name = "Cleo" },
                new Cat { Id = Guid.NewGuid(), Breed = "Ragdoll", Weight = 11, Name = "Whiskers" }
            };

            _catRepositoryMock.Setup(x => x.GetAll("Ragdoll", 10)).ReturnsAsync(expectedCats);

            // Act
            var result = await _handler.Handle(query, default);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(expectedCats));
            _catRepositoryMock.Verify(x => x.GetAll("Ragdoll", 10), Times.Once);
        }

        [Test]
        public async Task Handle_Should_ReturnEmptyCatList_When_NoCatWithCorrectBreed()
        {
            // Arrange
            var query = new GetAllCatsQuery(sortyByBreed: "NoBreed", 10);
            var expectedCats = new List<Cat>
            {

            };

            _catRepositoryMock.Setup(x => x.GetAll("NoBreed", 10)).ReturnsAsync(expectedCats);

            // Act
            var result = await _handler.Handle(query, default);

            // Assert
            Assert.That(result, Is.Empty);
            Assert.That(result, Is.EqualTo(expectedCats));
            _catRepositoryMock.Verify(x => x.GetAll("NoBreed", 10), Times.Once);
        }
    }
}
