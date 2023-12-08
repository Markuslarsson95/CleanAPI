using Application.Commands.Cats;
using Application.Commands.Birds.UpdateBird;
using Application.Dtos;
using FluentValidation.TestHelper;
using Application.Commands.Birds;

namespace Test.BirdTests.ValidatorTests
{
    [TestFixture]
    internal class UpdateBirdValidatorTests
    {
        private UpdateBirdByIdCommandValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new UpdateBirdByIdCommandValidator();
        }

        [Test]
        public void Validate_When_UpdateName_IsLessThanTwoCharachtersLong_ReturnsError()
        {
            // Arrange
            var command = new UpdateBirdByIdCommand(new BirdDto { Name = "U", CanFly = true }, Guid.NewGuid());

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.UpdatedBird.Name);
            result.ShouldNotHaveValidationErrorFor(x => x.UpdatedBird.CanFly);
        }

        [Test]
        public void Validate_When_UpdateName_IsMoreThanThirtyCharactersLong_ReturnsError()
        {
            // Arrange
            var command = new UpdateBirdByIdCommand(new BirdDto { Name = "Updaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaate", CanFly = true }, Guid.NewGuid());

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.UpdatedBird.Name);
            result.ShouldNotHaveValidationErrorFor(x => x.UpdatedBird.CanFly);
        }

        [Test]
        public void Validate_When_UpdateName_IsNull_ReturnsError()
        {
            // Arrange
            var command = new UpdateBirdByIdCommand(new BirdDto { Name = null!, CanFly = false }, Guid.NewGuid());

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.UpdatedBird.Name);
            result.ShouldNotHaveValidationErrorFor(x => x.UpdatedBird.CanFly);
        }

        [Test]
        public void Validate_When_UpdateBird_IsValid_ReturnsNoErrors()
        {
            // Arrange
            var command = new UpdateBirdByIdCommand(new BirdDto { Name = "Bird", CanFly = true }, Guid.NewGuid());

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.UpdatedBird.Name);
            result.ShouldNotHaveValidationErrorFor(x => x.UpdatedBird.CanFly);
        }
    }
}
