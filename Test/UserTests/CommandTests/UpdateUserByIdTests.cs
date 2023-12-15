using Application.Commands.Users.UpdateUser;
using Application.Dtos;
using Domain.Models;
using Infrastructure.Repositories;
using Moq;

namespace Test.UserTests.CommandTests
{
    [TestFixture]
    public class UpdateUserByIdTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private UpdateUserByIdCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _handler = new UpdateUserByIdCommandHandler(_userRepositoryMock.Object);
        }
        [Test]
        public async Task Handle_ValidUserId_UpdatesUserAndReturnsUpdatedUser()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var command = new UpdateUserByIdCommand(new UserDto { UserName = "NewUserName", Password = "NewPassword" }, userId);
            var userToUpdate = new User { Id = userId, UserName = "OldUserName", Password = "OldPassword" };

            _userRepositoryMock.Setup(repo => repo.GetById(userId)).ReturnsAsync(userToUpdate);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(command.UpdatedUser.UserName, Is.EqualTo(result.UserName));
            Assert.That(command.UpdatedUser.Password, Is.EqualTo(result.Password));
            _userRepositoryMock.Verify(repo => repo.GetById(userId), Times.Once);
            _userRepositoryMock.Verify(repo => repo.Update(userToUpdate), Times.Once);
        }

        [Test]
        public async Task Handle_InvalidUserId_ReturnsNull()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var updateUserByIdCommand = new UpdateUserByIdCommand(new UserDto { UserName = "NewUserName", Password = "NewPassword" }, userId);

            _userRepositoryMock.Setup(repo => repo.GetById(userId)).ReturnsAsync(null as User);

            // Act
            var result = await _handler.Handle(updateUserByIdCommand, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Null);
            _userRepositoryMock.Verify(repo => repo.GetById(userId), Times.Once);
            _userRepositoryMock.Verify(repo => repo.Update(It.IsAny<User>()), Times.Never);
        }
    }
}
