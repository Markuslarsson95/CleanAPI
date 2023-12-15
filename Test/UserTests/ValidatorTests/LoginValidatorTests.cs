using Application.Dtos;
using Application.Validators.UserValidators;
using FluentValidation.TestHelper;

namespace Test.UserTests.ValidatorTests
{
    [TestFixture]
    public class LoginValidatorTests
    {
        private LoginValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new LoginValidator();
        }

        [Test]
        public void Validate_ValidLogin_DoesNotHaveValidationError()
        {
            // Arrange
            var loginDto = new LoginDto
            {
                UserName = "ValidUsername",
                Password = "ValidPassword"
            };

            // Act
            var result = _validator.TestValidate(loginDto);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.UserName);
            result.ShouldNotHaveValidationErrorFor(x => x.Password);
        }

        [TestCase("", "ValidPassword", "Username required!")]
        public void Validate_InvalidUserName_ReturnsValidationError(string userName, string password, string expectedErrorMessage)
        {
            // Arrange
            var loginDto = new LoginDto
            {
                UserName = userName,
                Password = password
            };

            // Act
            var result = _validator.TestValidate(loginDto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.UserName).WithErrorMessage(expectedErrorMessage);
            result.ShouldNotHaveValidationErrorFor(x => x.Password);
        }

        [TestCase("ValidUsername", "", "Password required!")]
        public void Validate_InvalidPassword_ReturnsValidationError(string userName, string password, string expectedErrorMessage)
        {
            // Arrange
            var loginDto = new LoginDto
            {
                UserName = userName,
                Password = password
            };

            // Act
            var result = _validator.TestValidate(loginDto);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.UserName);
            result.ShouldHaveValidationErrorFor(x => x.Password).WithErrorMessage(expectedErrorMessage);
        }
    }
}
