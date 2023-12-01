using Application.Queries.Dogs.GetById;
using Infrastructure.Database;
using Infrastructure.RealDatabase;

namespace Test.DogTests.QueryTest
{
    [TestFixture]
    public class GetDogByIdTests
    {
        private GetDogByIdQueryHandler _handler;
        private MockDatabase _mockDatabase;
        private MySqlDB _mySqlDb;

        [SetUp]
        public void SetUp()
        {
            // Initialize the handler and mock database before each test
            _mockDatabase = new MockDatabase();
            _handler = new GetDogByIdQueryHandler(_mockDatabase, _mySqlDb);
        }

        [Test]
        public async Task Handle_ValidId_ReturnsCorrectDog()
        {
            // Arrange
            var dogId = new Guid("12345678-1234-5678-1234-567812345678");

            // Act
            var result = await _handler.Handle(new GetDogByIdQuery(dogId), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.That(result.Id, Is.EqualTo(dogId));
        }

        [Test]
        public async Task Handle_InvalidId_ReturnsNull()
        {
            // Arrange
            var invalidDogId = Guid.NewGuid();

            // Act
            var result = await _handler.Handle(new GetDogByIdQuery(invalidDogId), CancellationToken.None);

            // Assert
            Assert.IsNull(result);
        }
    }
}
