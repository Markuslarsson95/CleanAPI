using Application.Commands.Dogs;
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
        public async Task Handle_NewValidDog_ReturnsNewDog()
        {
            // Arrange
            Dog newDog = new()
            {
                Id = Guid.NewGuid(),
                Name = "testName"
            };

            DogDto newDogDto = new(){ Name = newDog.Name};
            var query = new AddDogCommand(newDogDto);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.EqualTo(newDog));
        }
    }
}

//using Application.Queries.Dogs.GetById;
//using Infrastructure.Database;

//namespace Test.DogTests.QueryTest
//{
//    [TestFixture]
//    public class GetDogByIdTests
//    {
//        private GetDogByIdQueryHandler _handler;
//        private MockDatabase _mockDatabase;

//        [SetUp]
//        public void SetUp()
//        {
//            // Initialize the handler and mock database before each test
//            _mockDatabase = new MockDatabase();
//            _handler = new GetDogByIdQueryHandler(_mockDatabase);
//        }

//        [Test]
//        public async Task Handle_ValidId_ReturnsCorrectDog()
//        {
//            // Arrange
//            var dogId = new Guid("12345678-1234-5678-1234-567812345678");

//            var query = new GetDogByIdQuery(dogId);

//            // Act
//            var result = await _handler.Handle(query, CancellationToken.None);

//            // Assert
//            Assert.NotNull(result);
//            Assert.That(result.Id, Is.EqualTo(dogId));
//        }

//        [Test]
//        public async Task Handle_InvalidId_ReturnsNull()
//        {
//            // Arrange
//            var invalidDogId = Guid.NewGuid();

//            var query = new GetDogByIdQuery(invalidDogId);

//            // Act
//            var result = await _handler.Handle(query, CancellationToken.None);

//            // Assert
//            Assert.IsNull(result);
//        }
//    }
//}