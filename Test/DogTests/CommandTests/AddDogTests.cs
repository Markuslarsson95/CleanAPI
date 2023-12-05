using Application.Commands.Dogs;
using Application.Dtos;
using Domain.Models;
using Domain.Repositories;
using Infrastructure.RealDatabase;
using Moq;

namespace Test.DogTests.CommandTests
{
    [TestFixture]
    public class AddDogTests
    {
        private Mock<IDogRepository> _dogRepositoryMock;
        private Mock<MySqlDB> _mySqlDbMock;
        private AddDogCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            //Initialize the handler and mock database before each test
            _mySqlDbMock = new Mock<MySqlDB>();
            _dogRepositoryMock = new Mock<IDogRepository>();
            _handler = new AddDogCommandHandler(_mySqlDbMock.Object, _dogRepositoryMock.Object);
        }

        [Test]
        public async Task Handle_AddNewValidDog_ReturnsNewDogList()
        {
            // Arrange
            var addDogCommand = new AddDogCommand(new DogDto { Name = "Test" });

            _dogRepositoryMock.Setup(x => x.Add(
                It.IsAny<Dog>()));

            // Act
            var result = await _handler.Handle(addDogCommand, default);

            // Assert
            _dogRepositoryMock.Verify(x => x.Add(It.Is<Dog>(d => d.Id == result.Id)), Times.Once);
        }
    }
}
