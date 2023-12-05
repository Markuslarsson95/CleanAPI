//using Application.Commands.Cats;
//using Application.Dtos;
//using Infrastructure.Database;

//namespace Test.CatTests.CommandTests
//{
//    [TestFixture]
//    public class AddCatTests
//    {
//        private AddCatCommandHandler _handler;
//        private MockDatabase _mockDatabase;

//        [SetUp]
//        public void SetUp()
//        {
//            //Initialize the handler and mock database before each test
//            _mockDatabase = new MockDatabase();
//            _handler = new AddCatCommandHandler(_mockDatabase);
//        }

//        [Test]
//        public async Task Handle_AddNewValidCat_ReturnsNewCatList()
//        {
//            // Arrange
//            var addCatCommand = new AddCatCommand(new CatDto { Name = "testNameCat", LikesToPlay = false });

//            // Act
//            var addedCat = await _handler.Handle(addCatCommand, CancellationToken.None);
//            var allCats = _mockDatabase.Cats;

//            // Assert
//            Assert.NotNull(addedCat);
//            Assert.Contains(addedCat, allCats);
//        }
//    }
//}
