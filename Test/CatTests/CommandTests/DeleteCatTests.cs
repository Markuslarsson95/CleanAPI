//using Application.Commands.Cats;
//using Infrastructure.Database;

//namespace Test.CatTests.CommandTests
//{
//    [TestFixture]
//    public class DeleteCatTests
//    {
//        private DeleteCatByIdCommandHandler _handler;
//        private MockDatabase _mockDatabase;

//        [SetUp]
//        public void SetUp()
//        {
//            //Initialize the handler and mock database before each test
//            _mockDatabase = new MockDatabase();
//            _handler = new DeleteCatByIdCommandHandler(_mockDatabase);
//        }

//        [Test]
//        public async Task Handle_DeleteCatValidId_RemovesCatFromList()
//        {
//            // Arrange
//            var deleteCatCommand = new DeleteCatByIdCommand(new Guid("12345678-1234-5678-1234-472756427786"));

//            // Act
//            var deletedCat = await _handler.Handle(deleteCatCommand, CancellationToken.None);
//            var catListAfterDeletion = _mockDatabase.Cats;

//            // Assert
//            Assert.NotNull(deletedCat);
//            Assert.That(catListAfterDeletion, Does.Not.Contain(deletedCat));
//        }

//        [Test]
//        public async Task Handle_DeleteCatInvalidId_ReturnsNull()
//        {
//            // Arrange
//            var deleteCatCommand = new DeleteCatByIdCommand(Guid.NewGuid());

//            /// Act
//            var deletedCat = await _handler.Handle(deleteCatCommand, CancellationToken.None);

//            // Assert
//            Assert.Null(deletedCat);
//        }
//    }
//}
