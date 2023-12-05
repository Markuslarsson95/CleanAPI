//using Application.Commands.Birds;
//using Application.Dtos;
//using Infrastructure.Database;

//namespace Test.BirdTests.CommandTests
//{
//    [TestFixture]
//    public class AddBirdTests
//    {
//        private AddBirdCommandHandler _handler;
//        private MockDatabase _mockDatabase;

//        [SetUp]
//        public void SetUp()
//        {
//            //Initialize the handler and mock database before each test
//            _mockDatabase = new MockDatabase();
//            _handler = new AddBirdCommandHandler(_mockDatabase);
//        }

//        [Test]
//        public async Task Handle_AddNewValidBird_ReturnsNewBirdList()
//        {
//            // Arrange
//            var addBirdCommand = new AddBirdCommand(new BirdDto { Name = "testNameBird", CanFly = true });

//            // Act
//            var addedBird = await _handler.Handle(addBirdCommand, CancellationToken.None);
//            var allBirds = _mockDatabase.Birds;

//            // Assert
//            Assert.NotNull(addedBird);
//            Assert.Contains(addedBird, allBirds);
//        }
//    }
//}
