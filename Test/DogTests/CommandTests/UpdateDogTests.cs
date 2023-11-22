using Application.Commands.Dogs.UpdateDog;
using Application.Dtos;
using Application.Queries.Dogs;
using Application.Queries.Dogs.GetAll;
using Domain.Models;
using Infrastructure.Database;

namespace Test.DogTests.CommandTests
{
    [TestFixture]
    public class UpdateDogTests
    {
        private UpdateDogByIdCommandHandler _handler;
        private GetAllDogsQueryHandler _allDogsHandler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            //Initialize the handler and mock database before each test
            _mockDatabase = new MockDatabase();
            _handler = new UpdateDogByIdCommandHandler(_mockDatabase);
            _allDogsHandler = new GetAllDogsQueryHandler(_mockDatabase);
        }

        [Test]
        public async Task Handle_UpdateDogValidId_ReturnsUpdatedDog()
        {
            // Arrange
            var dogId = new Guid("12345678-1234-5678-1234-867428755756");
            var dogDto = new DogDto()
            {
                Name = "TestUpdate"
            };

            var query = new GetAllDogsQuery();
            var command = new UpdateDogByIdCommand(dogDto, dogId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);
            //_mockDatabase.Dogs.Add(result);
            List<Dog> dogs = _mockDatabase.Dogs;
            var dogList = await _allDogsHandler.Handle(query, CancellationToken.None);


            // Assert
            Assert.NotNull(result);
            Assert.That(dogList, Is.EqualTo(dogs));
        }

        [Test]
        public async Task Handle_UpdateDogInvalidId_ReturnsNullReferenceException()
        {
            // Arrange
            var invalidDogId = new Guid("87654321-4321-8765-4321-098765432109");
            var dogDto = new DogDto()
            {
                Name = "TestUpdate"
            };

            var command = new UpdateDogByIdCommand(dogDto, invalidDogId);

            // Act & Assert
            Assert.ThrowsAsync<NullReferenceException>(
                async () => await _handler.Handle(command, CancellationToken.None));
        }
    }
}
