using Application.Commands.Birds;
using Application.Dtos;
using Domain.Models;
using Domain.Repositories;
using Moq;

namespace Test.BirdTests.CommandTests
{
    [TestFixture]
    public class UpdateBirdTests
    {
        private Mock<IGenericRepository<Bird>> _birdRepositoryMock;
        private UpdateBirdByIdCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            //Initialize the handler and mock database before each test
            _birdRepositoryMock = new Mock<IGenericRepository<Bird>>();
            _handler = new UpdateBirdByIdCommandHandler(_birdRepositoryMock.Object);
        }

        [Test]
        public async Task Handle_Should_UpdateBird_WhenIdIsValid()
        {
            // Arrange
            var command = new UpdateBirdByIdCommand(new BirdDto { Name = "Update", CanFly = true }, Guid.NewGuid());

            _birdRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(new Bird { Id = Guid.NewGuid(), Name = "Update", CanFly = true });
            _birdRepositoryMock.Setup(x => x.Update(It.IsAny<Bird>()));

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            Assert.That(result, Is.Not.Null);
            _birdRepositoryMock.Verify(x => x.Update(It.Is<Bird>(d => d.Name == result.Name)), Times.Once);
            _birdRepositoryMock.Verify(x => x.Update(It.Is<Bird>(d => d.Id == result.Id)), Times.Once);
            _birdRepositoryMock.Verify(x => x.Save(), Times.Once);
        }

        [Test]
        public async Task Handle_Should_Not_UpdateBird_WhenIdIsNotValid()
        {
            // Arrange
            var command = new UpdateBirdByIdCommand(new BirdDto { Name = "Update", CanFly = false }, Guid.NewGuid());

            _birdRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync((Bird)null!);
            _birdRepositoryMock.Setup(x => x.Update(It.IsAny<Bird>()));

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            Assert.That(result, Is.Null);
            _birdRepositoryMock.Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);
            _birdRepositoryMock.Verify(x => x.Update(It.IsAny<Bird>()), Times.Never);
            _birdRepositoryMock.Verify(x => x.Save(), Times.Never);
        }
    }
}