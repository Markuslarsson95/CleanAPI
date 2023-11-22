using Application.Queries.Dogs;
using Application.Queries.Dogs.GetAll;
using Domain.Models;
using Infrastructure.Database;

namespace Test.DogTests.QueryTest
{
    [TestFixture]
    public class GetAllDogsTests
    {
        private GetAllDogsQueryHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            // Initialize the handler and mock database before each test
            _mockDatabase = new MockDatabase();
            _handler = new GetAllDogsQueryHandler(_mockDatabase);
        }

        [Test]
        public async Task Handle_ValidList_ReturnsAllDogs()
        {
            // Arrange
            List<Dog> dogs = _mockDatabase.Dogs;

            var query = new GetAllDogsQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.That(result, Is.EqualTo(dogs));
        }

        //[Test]
        //public async Task Handle_InvalidList_ReturnsNotEqual()
        //{
        //    // Arrange
        //    List<Dog> dogs = _mockDatabase.Dogs;

        //    var query = new GetAllDogsQuery();

        //    // Act
        //    var result = await _handler.Handle(query, CancellationToken.None);

        //    dogs.Add(new Dog
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "Test-Fail",
        //    });

        //    // Assert
        //    Assert.That(result, Is.Not.EqualTo(dogs));
        //}
    }
}
