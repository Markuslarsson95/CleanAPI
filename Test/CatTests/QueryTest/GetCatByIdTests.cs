//using Application.Queries.Cats.GetById;
//using Infrastructure.Database;

//namespace Test.CatTests.QueryTest
//{
//    [TestFixture]
//    public class GetCatByIdTests
//    {
//        private GetCatByIdQueryHandler _handler;
//        private MockDatabase _mockDatabase;

//        [SetUp]
//        public void SetUp()
//        {
//            // Initialize the handler and mock database before each test
//            _mockDatabase = new MockDatabase();
//            _handler = new GetCatByIdQueryHandler(_mockDatabase);
//        }

//        [Test]
//        public async Task Handle_ValidId_ReturnsCorrectCat()
//        {
//            // Arrange
//            var catId = new Guid("12345678-1234-5678-1234-372653665237");

//            // Act
//            var result = await _handler.Handle(new GetCatByIdQuery(catId), CancellationToken.None);

//            // Assert
//            Assert.NotNull(result);
//            Assert.That(result.Id, Is.EqualTo(catId));
//        }

//        [Test]
//        public async Task Handle_InvalidId_ReturnsNull()
//        {
//            // Arrange
//            var invalidCatId = Guid.NewGuid();

//            // Act
//            var result = await _handler.Handle(new GetCatByIdQuery(invalidCatId), CancellationToken.None);

//            // Assert
//            Assert.IsNull(result);
//        }
//    }
//}
