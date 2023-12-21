using Application.Commands.Dogs.DeleteDog;
using Domain.Models.Animals;
using Infrastructure.Repositories.Dogs;
using Moq;

namespace Test.DogTests.CommandTests
{
    [TestFixture]
    public class DeleteDogTests
    {
        private Mock<IDogRepository> _dogRepositoryMock;
        private DeleteDogByIdCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            //Initialize the handler and mock database before each test
            _dogRepositoryMock = new Mock<IDogRepository>();
            _handler = new DeleteDogByIdCommandHandler(_dogRepositoryMock.Object);
        }

        [Test]
        public async Task Handle_Should_DeleteDog_WhenIdIsValid()
        {
            // Arrange
            var command = new DeleteDogByIdCommand(Guid.NewGuid());

            _dogRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(new Dog { Id = Guid.NewGuid(), Name = "Test" });
            _dogRepositoryMock.Setup(x => x.Delete(It.IsAny<Dog>()));

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            Assert.That(result, Is.Not.Null);
            _dogRepositoryMock.Verify(x => x.Delete(It.Is<Dog>(d => d.Id == result.Id)), Times.Once);
        }

        [Test]
        public async Task Handle_Should_Not_DeleteDog_WhenIdIsNotValid()
        {
            // Arrange
            var command = new DeleteDogByIdCommand(Guid.NewGuid());

            _dogRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync((Dog)null!);
            _dogRepositoryMock.Setup(x => x.Delete(It.IsAny<Dog>()));

            /// Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Null);
            _dogRepositoryMock.Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);
            _dogRepositoryMock.Verify(x => x.Delete(It.IsAny<Dog>()), Times.Never);
        }
    }
}