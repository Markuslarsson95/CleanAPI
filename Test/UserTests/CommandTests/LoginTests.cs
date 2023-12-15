using Application.Commands.Users.LoginUser;
using Application.Dtos;
using Infrastructure.Repositories;
using Moq;

namespace Test.UserTests.CommandTests
{
    [TestFixture]
    public class LoginTests
    {
        private Mock<ILoginRepository> _loginRepositoryMock;
        private LoginUserCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _loginRepositoryMock = new Mock<ILoginRepository>();
            _handler = new LoginUserCommandHandler(_loginRepositoryMock.Object);
        }

        [Test]
        public async Task Handle_Should_Return_JWT_When_LoginIsValid()
        {
            // Arrange
            var loginComand = new LoginUserCommand(new LoginDto { UserName = "user", Password = "password" });

            _loginRepositoryMock.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync("Token");

            // Act
            var result = await _handler.Handle(loginComand, default);

            // Assert
            Assert.That(result, Is.Not.Null);
            result.Equals("Token");
            _loginRepositoryMock.Verify(x => x.Login(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        }
    }
}
