using Application.Queries.Users.GetById;
using Domain.Models;
using Infrastructure.Repositories.Users;
using Moq;

namespace Test.UserTests.QueryTests
{
    [TestFixture]
    public class GetUserByIdTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private GetUserByIdQueryHandler _handler;

        [SetUp]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _handler = new GetUserByIdQueryHandler(_userRepositoryMock.Object);
        }
        [Test]
        public async Task Handle_ValidUserId_ReturnsUser()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var query = new GetUserByIdQuery(userId);
            var expectedUser = new User { Id = userId, UserName = "User", Password = "Password" };

            _userRepositoryMock.Setup(repo => repo.GetById(userId)).ReturnsAsync(expectedUser);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.EqualTo(expectedUser));
            _userRepositoryMock.Verify(repo => repo.GetById(userId), Times.Once);
        }

        [Test]
        public async Task Handle_InvalidUserId_ReturnsNull()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var query = new GetUserByIdQuery(userId);

            _userRepositoryMock.Setup(repo => repo.GetById(userId)).ReturnsAsync(null as User);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Null);
            _userRepositoryMock.Verify(repo => repo.GetById(userId), Times.Once);
        }
    }
}
