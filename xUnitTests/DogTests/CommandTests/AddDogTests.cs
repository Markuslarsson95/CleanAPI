using Application.Commands.Dogs;
using Application.Dtos;
using Domain.Models;
using Domain.Repositories;
using Infrastructure.RealDatabase;
using Moq;

namespace xUnitTests.DogTests.CommandTests
{
    public class AddDogTests
    {
        private readonly Mock<IDogRepository> _dogRepositoryMock;
        private readonly Mock<MySqlDB> _mySqlDbMock;

        public AddDogTests()
        {
            _dogRepositoryMock = new();
            _mySqlDbMock = new();
        }

        [Fact]
        public async Task Handle_Should_ReturnVerified_WhenNameIsValid()
        {
            // Arrange
            var command = new AddDogCommand(new DogDto { Name = "Test" });

            _dogRepositoryMock.Setup(x => x.Add(
                It.IsAny<Dog>()));

            var handler = new AddDogCommandHandler(_mySqlDbMock.Object, _dogRepositoryMock.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            _dogRepositoryMock.Verify(x => x.Add(It.Is<Dog>(d => d.Id == result.Id)), Times.Once);
        }
    }
}
