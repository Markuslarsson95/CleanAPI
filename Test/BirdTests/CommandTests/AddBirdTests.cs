using Application.Commands.Birds;
using Application.Dtos;
using Domain.Models;
using Domain.Repositories;
using Moq;

namespace Test.BirdTests.CommandTests
{
    [TestFixture]
    public class AddBirdTests
    {
        private Mock<IGenericRepository<Bird>> _birdRepositoryMock;
        private AddBirdCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            //Initialize the handler and mock database before each test
            _birdRepositoryMock = new Mock<IGenericRepository<Bird>>();
            _handler = new AddBirdCommandHandler(_birdRepositoryMock.Object);
        }

        [Test]
        public async Task Handle_Should_AddNewBird_WhenValid()
        {
            // Arrange
            var addBirdCommand = new AddBirdCommand(new BirdDto { Name = "Test" });

            _birdRepositoryMock.Setup(x => x.Add(
                It.IsAny<Bird>()));

            // Act
            var result = await _handler.Handle(addBirdCommand, default);

            // Assert
            Assert.That(result, Is.Not.Null);
            _birdRepositoryMock.Verify(x => x.Add(It.Is<Bird>(d => d.Id == result.Id)), Times.Once);
            _birdRepositoryMock.Verify(x => x.Save(), Times.Once);
        }
    }
}
