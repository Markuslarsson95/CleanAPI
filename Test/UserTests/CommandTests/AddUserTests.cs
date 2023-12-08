using Application.Commands.Users.AddUser;
using Application.Dtos;
using Domain.Models;
using Domain.Repositories;
using Moq;

namespace Test.UserTests.CommandTests
{
    [TestFixture]
    public class AddUserTests
    {
        private Mock<IGenericRepository<User>> _userRepositoryMock;
        private AddUserCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IGenericRepository<User>>();
            _handler = new AddUserCommandHandler(_userRepositoryMock.Object);
        }

        [Test]
        public async Task Handle_Should_AddNewUser_WhenValid()
        {
            // Arrange
            var addDogCommand = new AddUserCommand(new UserDto { UserName = "Test", Password = "Password" });

            _userRepositoryMock.Setup(x => x.Add(
                It.IsAny<User>()));

            // Act
            var result = await _handler.Handle(addDogCommand, default);

            // Assert
            Assert.That(result, Is.Not.Null);
            _userRepositoryMock.Verify(x => x.Add(It.Is<User>(d => d.Id == result.Id)), Times.Once);
            _userRepositoryMock.Verify(x => x.Save(), Times.Once);
        }
    }
}
