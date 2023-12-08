using Application.Commands.Cats;
using Application.Commands.Cats.AddCat;
using Application.Dtos;
using FluentValidation.TestHelper;

namespace Test.CatTests.ValidatorTests
{
    [TestFixture]
    public class AddCatValidatorTests
    {
        private AddCatCommandValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new AddCatCommandValidator();
        }

        [Test]
        public void Validate_When_Name_IsLessThanTwoCharachtersLong_ReturnsError()
        {
            // Arrange
            var command = new AddCatCommand(new CatDto { Name = "C", LikesToPlay = true});

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.NewCat.Name);
            result.ShouldNotHaveValidationErrorFor(x => x.NewCat.LikesToPlay);
        }

        [Test]
        public void Validate_When_Name_IsMoreThanThirtyCharactersLong_ReturnsError()
        {
            // Arrange
            var command = new AddCatCommand(new CatDto { Name = "Caaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaat", LikesToPlay = false });

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.NewCat.Name);
            result.ShouldNotHaveValidationErrorFor(x => x.NewCat.LikesToPlay);
        }

        [Test]
        public void Validate_When_Name_IsNull_ReturnsError()
        {
            // Arrange
            var command = new AddCatCommand(new CatDto { Name = null!, LikesToPlay = true });

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.NewCat.Name);
            result.ShouldNotHaveValidationErrorFor(x => x.NewCat.LikesToPlay);
        }

        [Test]
        public void Validate_When_NewCat_IsValid_ReturnsNoErrors()
        {
            // Arrange
            var command = new AddCatCommand(new CatDto { Name = "Cat", LikesToPlay = true });

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.NewCat.Name);
            result.ShouldNotHaveValidationErrorFor(x => x.NewCat.LikesToPlay);
        }
    }
}
