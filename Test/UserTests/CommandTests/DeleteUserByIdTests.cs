using Application.Commands.Users.DeleteUser;
using Domain.Models;
using Infrastructure.Repositories;
using Moq;

namespace Test.UserTests.CommandTests
{
    [TestFixture]
    public class DeleteUserByIdTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private DeleteUserByIdCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _handler = new DeleteUserByIdCommandHandler(_userRepositoryMock.Object);
        }
        [Test]
        public async Task Handle_ValidUserId_DeletesUserAndReturnsUser()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var command = new DeleteUserByIdCommand(userId);
            var userToDelete = new User { Id = userId, UserName = "DeleteUser", Password = "Password" };

            _userRepositoryMock.Setup(x => x.GetById(userId)).ReturnsAsync(userToDelete);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(userToDelete, Is.EqualTo(result));
            _userRepositoryMock.Verify(x => x.GetById(userId), Times.Once);
            _userRepositoryMock.Verify(x => x.Delete(userToDelete), Times.Once);
        }

        [Test]
        public async Task Handle_InvalidUserId_ReturnsNull()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var command = new DeleteUserByIdCommand(userId);

            _userRepositoryMock.Setup(x => x.GetById(userId)).ReturnsAsync(null as User);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Null);
            _userRepositoryMock.Verify(x => x.GetById(userId), Times.Once);
            _userRepositoryMock.Verify(x => x.Delete(It.IsAny<User>()), Times.Never);

        }
    }
}
