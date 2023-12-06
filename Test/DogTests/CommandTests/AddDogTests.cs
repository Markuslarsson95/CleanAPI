using Application.Commands.Dogs;
using Application.Dtos;
using Domain.Models;
using Domain.Repositories;
using Moq;

namespace Test.DogTests.CommandTests
{
    [TestFixture]
    public class AddDogTests
    {
        private Mock<IGenericRepository<Dog>> _dogRepositoryMock;
        private AddDogCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            //Initialize the handler and mock database before each test
            _dogRepositoryMock = new Mock<IGenericRepository<Dog>>();
            _handler = new AddDogCommandHandler(_dogRepositoryMock.Object);
        }

        [Test]
        public async Task Handle_Should_AddNewDog_WhenValid()
        {
            // Arrange
            var addDogCommand = new AddDogCommand(new DogDto { Name = "Test" });

            _dogRepositoryMock.Setup(x => x.Add(
                It.IsAny<Dog>()));

            // Act
            var result = await _handler.Handle(addDogCommand, default);

            // Assert
            Assert.That(result, Is.Not.Null);
            _dogRepositoryMock.Verify(x => x.Add(It.Is<Dog>(d => d.Id == result.Id)), Times.Once);
            _dogRepositoryMock.Verify(x => x.Save(), Times.Once);
        }
    }
}
