using Application.Dtos;
using Application.Validators.UserValidators;
using Domain.Models;
using FluentValidation.TestHelper;
using Infrastructure.Repositories.Users;
using Moq;

namespace Test.UserTests.ValidatorTests
{
    [TestFixture]
    public class AddUserCommandValidatorTests
    {
        private UserValidator _validator;
        private Mock<IUserRepository> _userRepositoryMock;

        [SetUp]
        public void SetUp()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _validator = new UserValidator(_userRepositoryMock.Object);
        }

        [Test]
        public void Validate_ValidUser_DoesNotHaveValidationError()
        {
            // Arrange
            var userDto = new UserDto
            {
                UserName = "ValidUsername",
                Password = "ValidPassword"
            };

            _userRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(new List<User> { new User { UserName = "User" } });

            // Act
            var result = _validator.TestValidate(userDto);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.UserName);
            result.ShouldNotHaveValidationErrorFor(x => x.Password);
        }

        [TestCase("", "ValidPassword", "Username can not be empty or null")]
        [TestCase("a", "ValidPassword", "Username must be at least two characters long")]
        [TestCase("TooLongUsernameThatIsMoreThan30Characters", "ValidPassword", "Username can not be more than 30 characters long")]
        [TestCase("Invalid@Username", "ValidPassword", "Username can only contain letters, numbers, underscores, and hyphens.")]
        [TestCase("ExistingUsername", "ValidPassword", "Username is already taken.")]
        public void Validate_InvalidUserName_ReturnsValidationError(string userName, string password, string expectedErrorMessage)
        {
            // Arrange
            var userDto = new UserDto
            {
                UserName = userName,
                Password = password
            };

            _userRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(new List<User> { new User { UserName = "ExistingUsername" } });

            // Act
            var result = _validator.TestValidate(userDto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.UserName).WithErrorMessage(expectedErrorMessage);
            result.ShouldNotHaveValidationErrorFor(x => x.Password);
        }

        [TestCase("ValidUsername", "", "Password can not be empty or null")]
        [TestCase("ValidUsername", "pw", "Password must be at least five characters long")]
        public void Validate_InvalidPassword_ReturnsValidationError(string userName, string password, string expectedErrorMessage)
        {
            // Arrange
            var userDto = new UserDto
            {
                UserName = userName,
                Password = password
            };

            _userRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(new List<User> { new User { UserName = "User" } });

            // Act
            var result = _validator.TestValidate(userDto);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.UserName);
            result.ShouldHaveValidationErrorFor(x => x.Password).WithErrorMessage(expectedErrorMessage);
        }
    }
}
