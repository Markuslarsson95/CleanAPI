using Application.Commands.Cats;
using Application.Commands.Cats.UpdateCat;
using Application.Dtos;
using FluentValidation.TestHelper;

namespace Test.CatTests.ValidatorTests
{
    [TestFixture]
    internal class UpdateCatValidatorTests
    {
        private UpdateCatByIdCommandValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new UpdateCatByIdCommandValidator();
        }

        [Test]
        public void Validate_When_UpdateName_IsLessThanTwoCharachtersLong_ReturnsError()
        {
            // Arrange
            var command = new UpdateCatByIdCommand(new CatDto { Name = "U", LikesToPlay = true}, Guid.NewGuid());

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.UpdatedCat.Name);
            result.ShouldNotHaveValidationErrorFor(x => x.UpdatedCat.LikesToPlay);
        }

        [Test]
        public void Validate_When_UpdateName_IsMoreThanThirtyCharactersLong_ReturnsError()
        {
            // Arrange
            var command = new UpdateCatByIdCommand(new CatDto { Name = "Updaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaate", LikesToPlay = false }, Guid.NewGuid());

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.UpdatedCat.Name);
            result.ShouldNotHaveValidationErrorFor(x => x.UpdatedCat.LikesToPlay);
        }

        [Test]
        public void Validate_When_UpdateName_IsNull_ReturnsError()
        {
            // Arrange
            var command = new UpdateCatByIdCommand(new CatDto { Name = null!, LikesToPlay = true }, Guid.NewGuid());

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.UpdatedCat.Name);
            result.ShouldNotHaveValidationErrorFor(x => x.UpdatedCat.LikesToPlay);
        }

        [Test]
        public void Validate_When_UpdateCat_IsValid_ReturnsNoErrors()
        {
            // Arrange
            var command = new UpdateCatByIdCommand(new CatDto { Name = "Cat", LikesToPlay = true }, Guid.NewGuid());

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.UpdatedCat.Name);
            result.ShouldNotHaveValidationErrorFor(x => x.UpdatedCat.LikesToPlay);
        }
    }
}
