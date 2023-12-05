//using Application.Commands.Cats;
//using Application.Dtos;
//using Application.Queries.Cats.GetAll;
//using Infrastructure.Database;

//namespace Test.CatTests.CommandTests
//{
//    [TestFixture]
//    public class UpdateCatTests
//    {
//        private UpdateCatByIdCommandHandler _handler;
//        private MockDatabase _mockDatabase;

//        [SetUp]
//        public void SetUp()
//        {
//            //Initialize the handler and mock database before each test
//            _mockDatabase = new MockDatabase();
//            _handler = new UpdateCatByIdCommandHandler(_mockDatabase);
//        }

//        [Test]
//        public async Task Handle_UpdateCatValidId_ReturnsUpdatedCatList()
//        {
//            // Arrange
//            var updateCatComand = new UpdateCatByIdCommand(new CatDto()
//            {
//                Name = "TestUpdateCat",
//                LikesToPlay = false
//            }, new Guid("12345678-1234-5678-1234-677758277550"));

//            // Act
//            var updatedCat = await _handler.Handle(updateCatComand, CancellationToken.None);
//            var catListAfterUpdate = _mockDatabase.Cats;

//            // Assert
//            Assert.NotNull(updatedCat);
//            Assert.That(catListAfterUpdate, Does.Contain(updatedCat));
//        }

//        [Test]
//        public async Task Handle_UpdateCatInvalidId_ReturnsNull()
//        {
//            // Arrange
//            var updateCatCommand = new UpdateCatByIdCommand(new CatDto()
//            {
//                Name = "TestUpdateCat",
//                LikesToPlay = false
//            }, Guid.NewGuid());

//            // Act
//            var updatedCat = await _handler.Handle(updateCatCommand, CancellationToken.None);

//            // Assert
//            Assert.IsNull(updatedCat);
//        }
//    }
//}
