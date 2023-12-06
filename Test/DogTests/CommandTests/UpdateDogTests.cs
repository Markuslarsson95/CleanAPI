using Application.Commands.Dogs.UpdateDog;
using Application.Dtos;
using Domain.Models;
using Domain.Repositories;
using Moq;

namespace Test.DogTests.CommandTests
{
    [TestFixture]
    public class UpdateDogTests
    {
        private Mock<IDogRepository> _dogRepositoryMock;
        private UpdateDogByIdCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            //Initialize the handler and mock database before each test
            _dogRepositoryMock = new Mock<IDogRepository>();
            _handler = new UpdateDogByIdCommandHandler(_dogRepositoryMock.Object);
        }

        [Test]
        public async Task Handle_Should_UpdateDog_WhenIdIsValid()
        {
            // Arrange
            var command = new UpdateDogByIdCommand(new DogDto { Name = "Update" }, Guid.NewGuid());

            _dogRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(new Dog { Id = Guid.NewGuid(), Name = "Update" });
            _dogRepositoryMock.Setup(x => x.Update(It.IsAny<Dog>()));

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            Assert.That(result, Is.Not.Null);
            _dogRepositoryMock.Verify(x => x.Update(It.Is<Dog>(d => d.Name == result.Name)), Times.Once);
            _dogRepositoryMock.Verify(x => x.Update(It.Is<Dog>(d => d.Id == result.Id)), Times.Once);
            _dogRepositoryMock.Verify(x => x.Save(), Times.Once);
        }

        [Test]
        public async Task Handle_Should_Not_UpdateDog_WhenIdIsNotValid()
        {
            // Arrange
            var command = new UpdateDogByIdCommand(new DogDto { Name = "Update" }, Guid.NewGuid());

            _dogRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync((Dog)null!);
            _dogRepositoryMock.Setup(x => x.Update(It.IsAny<Dog>()));

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            Assert.That(result, Is.Null);
            _dogRepositoryMock.Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);
            _dogRepositoryMock.Verify(x => x.Update(It.IsAny<Dog>()), Times.Never);
            _dogRepositoryMock.Verify(x => x.Save(), Times.Never);
        }
    }
}