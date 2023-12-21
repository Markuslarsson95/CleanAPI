using Application.Commands.Users.AddUser;
using Application.Dtos;
using Domain.Models;
using Infrastructure.RealDatabase;
using Infrastructure.Repositories.Password;
using Infrastructure.Repositories.Users;
using Moq;

namespace Test.UserTests.CommandTests
{
    [TestFixture]
    public class AddUserTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IPasswordEncryptor> _passwordEncryptorMock;
        private Mock<MySqlDB> _mySqlDbMock = new Mock<MySqlDB>();
        private AddUserCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _mySqlDbMock.Setup(x => x.Add(It.IsAny<User>()));
            _userRepositoryMock = new Mock<IUserRepository>();
            _passwordEncryptorMock = new Mock<IPasswordEncryptor>();
            _handler = new AddUserCommandHandler(_userRepositoryMock.Object, _passwordEncryptorMock.Object);
        }

        [Test]
        public async Task Handle_Should_AddNewUser_WhenValid()
        {
            // Arrange
            var userCommand = new AddUserCommand(new UserCreateDto { UserName = "User", Password = "Password", Birds = new List<BirdDto> { }, Cats = new List<CatDto> { }, Dogs = new List<DogDto> { } });

            // Act
            var result = await _handler.Handle(userCommand, default);

            // Assert
            Assert.That(result, Is.Not.Null);
            _userRepositoryMock.Verify(x => x.Add(It.Is<User>(u => u.Id == result.Id
            && u.UserName == result.UserName && u.Password == result.Password)), Times.Once);
        }
    }
}
