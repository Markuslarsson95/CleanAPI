using Application.Commands.Users.UpdateUser;
using Application.Dtos;
using Domain.Models;
using Infrastructure.Repositories.Password;
using Infrastructure.Repositories.Users;
using Moq;

namespace Test.UserTests.CommandTests
{
    [TestFixture]
    public class UpdateUserByIdTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IPasswordEncryptor> _passwordEncryptorMock;
        private UpdateUserByIdCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _passwordEncryptorMock = new Mock<IPasswordEncryptor>();
            _handler = new UpdateUserByIdCommandHandler(_userRepositoryMock.Object, _passwordEncryptorMock.Object);
        }
        [Test]
        public async Task Handle_ValidUserId_UpdatesUserAndReturnsUpdatedUser()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var request = new UpdateUserByIdCommand(new UserDto { UserName = "NewUserName", Password = "NewPassword" }, userId);
            var existingUser = new User { Id = userId, UserName = "OldUserName", Password = "OldPassword" };

            _userRepositoryMock.Setup(x => x.GetById(userId)).ReturnsAsync(existingUser);
            _passwordEncryptorMock.Setup(x => x.Encrypt(request.UpdatedUser.Password)).Returns("EncryptedPassword");

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(userId));
            Assert.That(result.UserName, Is.EqualTo(request.UpdatedUser.UserName));
            Assert.That(result.Password, Is.EqualTo("EncryptedPassword"));
            _userRepositoryMock.Verify(x => x.Update(existingUser), Times.Once);
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
