using Application.Dtos;
using Application.Validators.DogValidators;
using FluentValidation.TestHelper;

namespace Test.DogTests.ValidatorTests
{
    [TestFixture]
    public class DogValidatorTests
    {
        private DogValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new DogValidator();
        }

        [Test]
        public void Validate_When_Name_IsLessThanTwoCharachtersLong_ReturnsError()
        {
            // Arrange
            var dogDto = new DogDto { Name = "T", Breed = "Bulldog", Weight = 30 };

            // Act
            var result = _validator.TestValidate(dogDto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
            result.ShouldNotHaveValidationErrorFor(x => x.Breed);
            result.ShouldNotHaveValidationErrorFor(x => x.Weight);
        }

        [Test]
        public void Validate_When_Name_IsMoreThanThirtyCharactersLong_ReturnsError()
        {
            // Arrange
            var dogDto = new DogDto { Name = "Teeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeest", Breed = "Pitbull", Weight = 25 };

            // Act
            var result = _validator.TestValidate(dogDto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
            result.ShouldNotHaveValidationErrorFor(x => x.Breed);
            result.ShouldNotHaveValidationErrorFor(x => x.Weight);
        }

        [Test]
        public void Validate_When_Name_IsNull_ReturnsError()
        {
            // Arrange
            var dogDto = new DogDto { Name = null!, Breed = "Cocker Spaniel", Weight = 10 };

            // Act
            var result = _validator.TestValidate(dogDto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
            result.ShouldNotHaveValidationErrorFor(x => x.Breed);
            result.ShouldNotHaveValidationErrorFor(x => x.Weight);
        }

        [Test]
        public void Validate_When_Breed_IsNull_ReturnsError()
        {
            // Arrange
            var dogDto = new DogDto { Name = "Dog", Weight = 10 };

            // Act
            var result = _validator.TestValidate(dogDto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Breed);
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
            result.ShouldNotHaveValidationErrorFor(x => x.Weight);
        }

        [Test]
        public void Validate_When_Weight_IsNull_ReturnsError()
        {
            // Arrange
            var dogDto = new DogDto { Name = "Dog", Breed = "Cocker Spaniel" };

            // Act
            var result = _validator.TestValidate(dogDto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Weight);
            result.ShouldNotHaveValidationErrorFor(x => x.Breed);
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }

        [Test]
        public void Validate_When_NewDog_IsValid_ReturnsNoErrors()
        {
            // Arrange
            var dogDto = new DogDto { Name = "Dog", Breed = "English Bulldog", Weight = 30 };

            // Act
            var result = _validator.TestValidate(dogDto);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
            result.ShouldNotHaveValidationErrorFor(x => x.Breed);
            result.ShouldNotHaveValidationErrorFor(x => x.Weight);
        }
    }
}
