using Application.Commands.Dogs;
using Application.Dtos;
using Domain.Models.Animal;
using Infrastructure.Repositories;
using Moq;

namespace Test.DogTests.CommandTests
{
    [TestFixture]
    public class AddDogTests
    {
        private Mock<IDogRepository> _dogRepositoryMock;
        private AddDogCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _dogRepositoryMock = new Mock<IDogRepository>();
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
        }
    }
}
