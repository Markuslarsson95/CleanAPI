using Application.Commands.Dogs.DeleteDog;
using Domain.Models;
using Domain.Repositories;
using Infrastructure.RealDatabase;
using Moq;

namespace xUnitTests.DogTests.CommandTests
{
    public class DeleteDogTests
    {
        private readonly Mock<IDogRepository> _dogRepositoryMock;
        private readonly Mock<MySqlDB> _mySqlDbMock;
        public DeleteDogTests()
        {
            _dogRepositoryMock = new();
            _mySqlDbMock = new();
        }
        [Fact]
        public async Task Handle_Should_ReturnVerified_WhenGuidIsValid()
        {
            // Arrange
            var command = new DeleteDogByIdCommand(Guid.NewGuid());

            _dogRepositoryMock.Setup(x => x.Delete(
                It.IsAny<Dog>()));

            var handler = new DeleteDogByIdCommandHandler(_mySqlDbMock.Object, _dogRepositoryMock.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            _dogRepositoryMock.Verify(x => x.Delete(It.Is<Dog>(d => d.Id == result.Id)), Times.Once);
        }
    }
}
