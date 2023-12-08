using Application.Commands.Dogs.AddDog;
using Application.Commands.Users.AddUser;
using Application.Dtos;
using FluentValidation.TestHelper;

namespace Test.UserTests.ValidatorTests
{
    [TestFixture]
    public class AddUserValidatorTests
    {
        private AddUserCommandValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new AddUserCommandValidator();
        }

        [Test]
        public void Validate_When_Name_IsLessThanTwoCharachtersLong_ReturnsError()
        {
            // Arrange
            var command = new AddUserCommand(new UserDto { UserName = "T", Password = "Password" });

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.NewUser.UserName);
        }

        [Test]
        public void Validate_When_Name_IsMoreThanThirtyCharactersLong_ReturnsError()
        {
            // Arrange
            var command = new AddUserCommand(new UserDto { UserName = "Teeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeest", Password = "Password" });

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.NewUser.UserName);
        }

        [Test]
        public void Validate_When_Name_IsNull_ReturnsError()
        {
            // Arrange
            var command = new AddUserCommand(new UserDto { UserName = null!, Password = "Password" });

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.NewUser.UserName);
        }

        [Test]
        public void Validate_When_Password_IsNull_ReturnsError()
        {
            // Arrange
            var command = new AddUserCommand(new UserDto { UserName = "User", Password = null! });

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.NewUser.Password);
        }

        [Test]
        public void Validate_When_NewUser_IsValid_ReturnsNoErrors()
        {
            // Arrange
            var command = new AddUserCommand(new UserDto { UserName = "User", Password = "Password" });

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.NewUser.UserName);
        }
    }
}
