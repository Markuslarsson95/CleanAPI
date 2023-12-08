using Application.Commands.Birds;
using Application.Commands.Birds.AddBird;
using Application.Dtos;
using FluentValidation.TestHelper;

namespace Test.BirdTests.ValidatorTests
{
    [TestFixture]
    public class AddBirdValidatorTests
    {
        private AddBirdCommandValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new AddBirdCommandValidator();
        }

        [Test]
        public void Validate_When_Name_IsLessThanTwoCharachtersLong_ReturnsError()
        {
            // Arrange
            var command = new AddBirdCommand(new BirdDto { Name = "B", CanFly = true });

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.NewBird.Name);
            result.ShouldNotHaveValidationErrorFor(x => x.NewBird.CanFly);
        }

        [Test]
        public void Validate_When_Name_IsMoreThanThirtyCharactersLong_ReturnsError()
        {
            // Arrange
            var command = new AddBirdCommand(new BirdDto { Name = "Biiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiird", CanFly = true });

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.NewBird.Name);
            result.ShouldNotHaveValidationErrorFor(x => x.NewBird.CanFly);
        }

        [Test]
        public void Validate_When_Name_IsNull_ReturnsError()
        {
            // Arrange
            var command = new AddBirdCommand(new BirdDto { Name = null!, CanFly = true });

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.NewBird.Name);
            result.ShouldNotHaveValidationErrorFor(x => x.NewBird.CanFly);
        }

        [Test]
        public void Validate_When_NewBird_IsValid_ReturnsNoErrors()
        {
            // Arrange
            var command = new AddBirdCommand(new BirdDto { Name = "Bird", CanFly = true });

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.NewBird.Name);
            result.ShouldNotHaveValidationErrorFor(x => x.NewBird.CanFly);
        }
    }
}
