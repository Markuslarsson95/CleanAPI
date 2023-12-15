using Application.Queries.Users.GetAll;
using Application.Queries.Users;
using Domain.Models;
using Infrastructure.Repositories;
using Moq;
using Domain.Models.Animals;

namespace Test.UserTests.QueryTests
{
    [TestFixture]
    public class GetAllUsersTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private GetAllUsersQueryHandler _handler;

        [SetUp]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _handler = new GetAllUsersQueryHandler(_userRepositoryMock.Object);
        }
        [Test]
        public async Task Handle_ReturnsListOfUsers()
        {
            // Arrange
            var query = new GetAllUsersQuery();

            var expectedUsers = new List<User>
            {
                 new User { Id = Guid.NewGuid(), UserName = "User", Password = "Password", Animals = new List<Animal>{ } },
                 new User { Id = Guid.NewGuid(), UserName = "User2", Password = "Password2", Animals = new List<Animal>{ } }
            };

            _userRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(expectedUsers);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.EqualTo(expectedUsers));
            _userRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
        }

        [Test]
        public async Task Handle_EmptyUserList_ReturnsEmptyList()
        {
            // Arrange
            var query = new GetAllUsersQuery();

            _userRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(new List<User>());

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
            _userRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
        }
    }
}
