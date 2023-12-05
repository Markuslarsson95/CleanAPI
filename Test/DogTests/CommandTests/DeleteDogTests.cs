using Application.Commands.Dogs.DeleteDog;
using Domain.Models;
using Domain.Repositories;
using Infrastructure.RealDatabase;
using Moq;
using MySqlX.XDevAPI.Common;

namespace Test.DogTests.CommandTests
{
    [TestFixture]
    public class DeleteDogTests
    {
        private Mock<IDogRepository> _dogRepositoryMock;
        private Mock<MySqlDB> _mySqlDbMock;
        private DeleteDogByIdCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            //Initialize the handler and mock database before each test
            _dogRepositoryMock = new Mock<IDogRepository>();
            _mySqlDbMock = new Mock<MySqlDB>();
            _handler = new DeleteDogByIdCommandHandler(_mySqlDbMock.Object, _dogRepositoryMock.Object);
        }

        [Test]
        public async Task Handle_DeleteDogValidId_RemovesDogFromList()
        {
            // Arrange
            var deleteDogCommand = new DeleteDogByIdCommand(Guid.NewGuid());

            _dogRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(new Dog { Id = Guid.NewGuid(), Name = "Test"});
            _dogRepositoryMock.Setup(x => x.Delete(It.IsAny<Dog>()));

            // Act
            await _handler.Handle(deleteDogCommand, default);

            // Assert
            _dogRepositoryMock.Verify(x => x.Delete(It.IsAny<Dog>()), Times.Once);
        }

        //[Test]
        //public async Task Handle_DeleteDogInvalidId_ReturnsNull()
        //{
        //    // Arrange
        //    var deleteDogCommand = new DeleteDogByIdCommand(Guid.NewGuid());

        //    /// Act
        //    var deletedDog = await _handler.Handle(deleteDogCommand, CancellationToken.None);

        //    // Assert
        //    Assert.Null(deletedDog);
        //}
    }
}