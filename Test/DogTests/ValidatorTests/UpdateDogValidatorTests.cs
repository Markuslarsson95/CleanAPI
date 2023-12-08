using Application.Commands.Cats;
using Application.Commands.Dogs.UpdateDog;
using Application.Dtos;
using FluentValidation.TestHelper;

namespace Test.DogTests.ValidatorTests
{
    [TestFixture]
    internal class UpdateDogValidatorTests
    {
        private UpdateDogByIdCommandValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new UpdateDogByIdCommandValidator();
        }

        [Test]
        public void Validate_When_UpdateName_IsLessThanTwoCharachtersLong_ReturnsError()
        {
            // Arrange
            var command = new UpdateDogByIdCommand(new DogDto { Name = "U" }, Guid.NewGuid());

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.UpdatedDog.Name);
        }

        [Test]
        public void Validate_When_UpdateName_IsMoreThanThirtyCharactersLong_ReturnsError()
        {
            // Arrange
            var command = new UpdateDogByIdCommand(new DogDto { Name = "Updaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaate" }, Guid.NewGuid());

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.UpdatedDog.Name);
        }

        [Test]
        public void Validate_When_UpdateName_IsNull_ReturnsError()
        {
            // Arrange
            var command = new UpdateDogByIdCommand(new DogDto { Name = null! }, Guid.NewGuid());

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.UpdatedDog.Name);
        }

        [Test]
        public void Validate_When_UpdateDog_IsValid_ReturnsNoErrors()
        {
            // Arrange
            var command = new UpdateDogByIdCommand(new DogDto { Name = "Dog" }, Guid.NewGuid());

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.UpdatedDog.Name);
        }
    }
}
