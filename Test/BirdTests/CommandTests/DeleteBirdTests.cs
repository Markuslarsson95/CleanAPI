﻿using Application.Commands.Birds;
using Domain.Models.Animals;
using Infrastructure.Repositories.Birds;
using Moq;

namespace Test.BirdTests.CommandTests
{
    [TestFixture]
    public class DeleteBirdTests
    {
        private Mock<IBirdRepository> _birdRepositoryMock;
        private DeleteBirdByIdCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            //Initialize the handler and mock database before each test
            _birdRepositoryMock = new Mock<IBirdRepository>();
            _handler = new DeleteBirdByIdCommandHandler(_birdRepositoryMock.Object);
        }

        [Test]
        public async Task Handle_Should_DeleteBird_WhenIdIsValid()
        {
            // Arrange
            var command = new DeleteBirdByIdCommand(Guid.NewGuid());

            _birdRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(new Bird { Id = Guid.NewGuid(), Name = "Test", CanFly = true });
            _birdRepositoryMock.Setup(x => x.Delete(It.IsAny<Bird>()));

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            Assert.That(result, Is.Not.Null);
            _birdRepositoryMock.Verify(x => x.Delete(It.Is<Bird>(d => d.Id == result.Id)), Times.Once);
        }

        [Test]
        public async Task Handle_Should_Not_DeleteBird_WhenIdIsNotValid()
        {
            // Arrange
            var command = new DeleteBirdByIdCommand(Guid.NewGuid());

            _birdRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync((Bird)null!);
            _birdRepositoryMock.Setup(x => x.Delete(It.IsAny<Bird>()));

            /// Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Null);
            _birdRepositoryMock.Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);
            _birdRepositoryMock.Verify(x => x.Delete(It.IsAny<Bird>()), Times.Never);
        }
    }
}