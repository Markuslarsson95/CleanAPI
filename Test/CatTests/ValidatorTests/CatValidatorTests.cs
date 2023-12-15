using Application.Dtos;
using Application.Validators.CatValidators;
using FluentValidation.TestHelper;

namespace Test.CatTests.ValidatorTests
{
    [TestFixture]
    public class CatValidatorTests
    {
        private CatValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new CatValidator();
        }

        [Test]
        public void Validate_When_Name_IsLessThanTwoCharachtersLong_ReturnsError()
        {
            // Arrange
            var catDto = new CatDto { Name = "C", LikesToPlay = true, Breed = "Ragdoll", Weight = 10 };

            // Act
            var result = _validator.TestValidate(catDto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
            result.ShouldNotHaveValidationErrorFor(x => x.LikesToPlay);
            result.ShouldNotHaveValidationErrorFor(x => x.Breed);
            result.ShouldNotHaveValidationErrorFor(x => x.Weight);
        }

        [Test]
        public void Validate_When_Name_IsMoreThanThirtyCharactersLong_ReturnsError()
        {
            // Arrange
            var catDto = new CatDto { Name = "Caaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaat", LikesToPlay = false, Breed = "Ragdoll", Weight = 5 };

            // Act
            var result = _validator.TestValidate(catDto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
            result.ShouldNotHaveValidationErrorFor(x => x.LikesToPlay);
            result.ShouldNotHaveValidationErrorFor(x => x.Breed);
            result.ShouldNotHaveValidationErrorFor(x => x.Weight);
        }

        [Test]
        public void Validate_When_Name_IsNull_ReturnsError()
        {
            // Arrange
            var catDto = new CatDto { Name = null!, LikesToPlay = true, Breed = "Maine Coon", Weight = 12 };

            // Act
            var result = _validator.TestValidate(catDto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
            result.ShouldNotHaveValidationErrorFor(x => x.LikesToPlay);
            result.ShouldNotHaveValidationErrorFor(x => x.Breed);
            result.ShouldNotHaveValidationErrorFor(x => x.Weight);
        }

        [Test]
        public void Validate_When_Breed_IsNull_ReturnsError()
        {
            // Arrange
            var catDto = new CatDto { Name = "Cat", LikesToPlay = true, Breed = null!, Weight = 12 };

            // Act
            var result = _validator.TestValidate(catDto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Breed);
            result.ShouldNotHaveValidationErrorFor(x => x.LikesToPlay);
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
            result.ShouldNotHaveValidationErrorFor(x => x.Weight);
        }

        [Test]
        public void Validate_When_Weight_IsNull_ReturnsError()
        {
            // Arrange
            var catDto = new CatDto { Name = "Cat", LikesToPlay = true, Breed = "Maine Coon" };

            // Act
            var result = _validator.TestValidate(catDto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Weight);
            result.ShouldNotHaveValidationErrorFor(x => x.LikesToPlay);
            result.ShouldNotHaveValidationErrorFor(x => x.Breed);
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }

        [Test]
        public void Validate_When_NewCat_IsValid_ReturnsNoErrors()
        {
            // Arrange
            var catDto = new CatDto { Name = "Cat", LikesToPlay = true, Breed = "Ragdoll", Weight = 10 };

            // Act
            var result = _validator.TestValidate(catDto);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
            result.ShouldNotHaveValidationErrorFor(x => x.LikesToPlay);
            result.ShouldNotHaveValidationErrorFor(x => x.Weight);
            result.ShouldNotHaveValidationErrorFor(x => x.Breed);
        }
    }
}
