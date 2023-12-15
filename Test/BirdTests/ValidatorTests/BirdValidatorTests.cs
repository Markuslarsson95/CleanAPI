using Application.Dtos;
using Application.Validators.BirdValidators;
using FluentValidation.TestHelper;

namespace Test.BirdTests.ValidatorTests
{
    [TestFixture]
    public class BirdValidatorTests
    {
        private BirdValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new BirdValidator();
        }

        [Test]
        public void Validate_When_Name_IsLessThanTwoCharachtersLong_ReturnsError()
        {
            // Arrange
            var birdDto = new BirdDto { Name = "B", CanFly = true, Color = "Blue" };

            // Act
            var result = _validator.TestValidate(birdDto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
            result.ShouldNotHaveValidationErrorFor(x => x.CanFly);
            result.ShouldNotHaveValidationErrorFor(x => x.Color);
        }

        [Test]
        public void Validate_When_Name_IsMoreThanThirtyCharactersLong_ReturnsError()
        {
            // Arrange
            var birdDto = new BirdDto { Name = "Biiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiird", CanFly = true, Color = "Yellow" };

            // Act
            var result = _validator.TestValidate(birdDto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
            result.ShouldNotHaveValidationErrorFor(x => x.CanFly);
            result.ShouldNotHaveValidationErrorFor(x => x.Color);
        }

        [Test]
        public void Validate_When_Name_IsNull_ReturnsError()
        {
            // Arrange
            var birdDto = new BirdDto { Name = null!, CanFly = true, Color = "Green" };

            // Act
            var result = _validator.TestValidate(birdDto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
            result.ShouldNotHaveValidationErrorFor(x => x.CanFly);
            result.ShouldNotHaveValidationErrorFor(x => x.Color);
        }

        [Test]
        public void Validate_When_Color_IsNull_ReturnsError()
        {
            // Arrange
            var birdDto = new BirdDto { Name = "Bird", CanFly = true, Color = null! };

            // Act
            var result = _validator.TestValidate(birdDto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Color);
            result.ShouldNotHaveValidationErrorFor(x => x.CanFly);
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }

        [Test]
        public void Validate_When_NewBird_IsValid_ReturnsNoErrors()
        {
            // Arrange
            var birdDto = new BirdDto { Name = "Bird", CanFly = true, Color = "Blue" };

            // Act
            var result = _validator.TestValidate(birdDto);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
            result.ShouldNotHaveValidationErrorFor(x => x.CanFly);
            result.ShouldNotHaveValidationErrorFor(x => x.Color);
        }
    }
}
