using Application.Commands.Dogs.AddDog;
using Application.Dtos;
using FluentValidation.TestHelper;

namespace Test.DogTests.ValidatorTests
{
    [TestFixture]
    public class AddDogValidatorTests
    {
        private AddDogCommandValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new AddDogCommandValidator();
        }

        [Test]
        public void Validate_When_Name_IsLessThanTwoCharachtersLong_ReturnsError()
        {
            // Arrange
            var command = new DogDto { Name = "T" };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Test]
        public void Validate_When_Name_IsMoreThanThirtyCharactersLong_ReturnsError()
        {
            // Arrange
            var command = new DogDto { Name = "Teeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeest" };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Test]
        public void Validate_When_Name_IsNull_ReturnsError()
        {
            // Arrange
            var command = new DogDto { Name = null! };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Test]
        public void Validate_When_NewDog_IsValid_ReturnsNoErrors()
        {
            // Arrange
            var command = new DogDto { Name = "Dog" };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }
    }
}
