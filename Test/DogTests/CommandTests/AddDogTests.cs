using Application.Commands.Dogs;
using Application.Commands.Dogs.AddDog;
using Application.Dtos;
using Domain.Models;
using Infrastructure.Database;

namespace Test.DogTests.CommandTests
{
    [TestFixture]
    public class AddDogTests
    {
        private AddDogCommandHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            //Initialize the handler and mock database before each test
            _mockDatabase = new MockDatabase();
            _handler = new AddDogCommandHandler(_mockDatabase);
        }

        [Test]
        public async Task Handle_AddNewValidDog_ReturnsNewDogList()
        {
            // Arrange
            var addDogCommand = new AddDogCommand(new DogDto { Name = "testNameDog" });

            // Act
            var addedDog = await _handler.Handle(addDogCommand, CancellationToken.None);
            var allDogs = _mockDatabase.Dogs;

            // Assert
            Assert.NotNull(addedDog);
            Assert.Contains(addedDog, allDogs);
        }
        
        [Test]
        public async Task Handle_AddNewInValidDog_ReturnsValidatonIsFalse()
        {
            // Arrange
            var invalidAddDogCommand = new AddDogCommand(new DogDto { Name = "d" });
            var validator = new AddDogCommandValidator();
            var addedDog = new Dog();

            // Act
            var validatorResult = await validator.ValidateAsync(invalidAddDogCommand);
            if(validatorResult.IsValid)
            {
                addedDog = await _handler.Handle(invalidAddDogCommand, CancellationToken.None);
            }
            var allDogs = _mockDatabase.Dogs;

            // Assert
            Assert.IsFalse(validatorResult.IsValid);
            Assert.That(allDogs, Does.Not.Contain(addedDog));
        }
    }
}
