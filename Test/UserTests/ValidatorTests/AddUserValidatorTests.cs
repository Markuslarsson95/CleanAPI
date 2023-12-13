//using Application.Commands.Users.AddUser;
//using Application.Dtos;
//using Domain.Models;
//using Domain.Repositories;
//using FluentValidation.TestHelper;
//using Moq;

//namespace Test.UserTests.ValidatorTests
//{
//    [TestFixture]
//    public class AddUserCommandValidatorTests
//    {
//        [TestFixture]
//        public class AddUserValidatorTests
//        {
//            private Mock<IUserRepository> _userRepositoryMock;
//            private AddUserCommandValidator _validator;
//            private Mock<AddUserCommandValidator> _validatorMock;

//            [SetUp]
//            public void SetUp()
//            {
//                _userRepositoryMock.Setup(x => x.Add(It.IsAny<User>()));
//                _userRepositoryMock = new Mock<IUserRepository>();
//                _validator = new AddUserCommandValidator(_userRepositoryMock.Object);
//                _validatorMock = new Mock<AddUserCommandValidator>();
//            }

//            [Test]
//            public void Validate_When_Name_IsLessThanTwoCharachtersLong_ReturnsError()
//            {
//                // Arrange
//                var command = new AddUserCommand(new UserDto { UserName = "T", Password = "Password" });

//                _validatorMock.Setup(x => x.BeUniqueUsername(It.IsAny<string>())).Returns(true);

//                // Act
//                var result = _validator.TestValidate(command);

//                // Assert
//                result.ShouldHaveValidationErrorFor(x => x.NewUser.UserName)
//                      .WithErrorMessage("Username must be at least two characters long");
//            }

//            [Test]
//            public void Validate_When_Name_IsMoreThanThirtyCharactersLong_ReturnsError()
//            {
//                // Arrange
//                var command = new AddUserCommand(new UserDto { UserName = "Teeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeest", Password = "Password" });

//                // Act
//                var result = _validator.TestValidate(command);

//                // Assert
//                result.ShouldHaveValidationErrorFor(x => x.NewUser.UserName)
//                    .WithErrorMessage("Username can not be more than 30 characters long");
//            }

//            [Test]
//            public void Validate_When_Name_IsNull_ReturnsError()
//            {
//                // Arrange
//                var command = new AddUserCommand(new UserDto { UserName = null!, Password = "Password" });

//                // Act
//                var result = _validator.TestValidate(command);

//                // Assert
//                result.ShouldHaveValidationErrorFor(x => x.NewUser.UserName)
//                    .WithErrorMessage("Username can not be empty or null");
//            }

//            [Test]
//            public void Validate_When_Password_IsNull_ReturnsError()
//            {
//                // Arrange
//                var command = new AddUserCommand(new UserDto { UserName = "User", Password = null! });

//                // Act
//                var result = _validator.TestValidate(command);

//                // Assert
//                result.ShouldHaveValidationErrorFor(x => x.NewUser.Password)
//                    .WithErrorMessage("Password can not be empty or null");
//            }

//            [Test]
//            public void Validate_When_NewUser_IsValid_ReturnsNoErrors()
//            {
//                // Arrange
//                var command = new AddUserCommand(new UserDto { UserName = "User", Password = "Password" });

//                // Act
//                var result = _validator.TestValidate(command);

//                // Assert
//                result.ShouldNotHaveValidationErrorFor(x => x.NewUser.UserName);
//                result.ShouldNotHaveValidationErrorFor(x => x.NewUser.Password);
//            }
//        }
//    }
//}
