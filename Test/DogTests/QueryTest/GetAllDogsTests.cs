//using Application.Queries.Dogs;
//using Application.Queries.Dogs.GetAll;
//using Domain.Models;
//using Infrastructure.Database;
//using Infrastructure.RealDatabase;

//namespace Test.DogTests.QueryTest
//{
//    [TestFixture]
//    public class GetAllDogsTests
//    {
//        private GetAllDogsQueryHandler _handler;
//        private MockDatabase _mockDatabase;
//        private MySqlDB _mySqlDb;

//        [SetUp]
//        public void SetUp()
//        {
//            // Initialize the handler and mock database before each test
//            _mockDatabase = new MockDatabase();
//            _handler = new GetAllDogsQueryHandler(_mockDatabase, _mySqlDb);
//        }

//        [Test]
//        public async Task Handle_ValidList_ReturnsAllDogs()
//        {
//            // Arrange
//            List<Dog> dogs = _mockDatabase.Dogs;

//            // Act
//            var result = await _handler.Handle(new GetAllDogsQuery(), CancellationToken.None);

//            // Assert
//            Assert.NotNull(result);
//            Assert.That(result, Is.EqualTo(dogs));
//        }
//    }
//}
