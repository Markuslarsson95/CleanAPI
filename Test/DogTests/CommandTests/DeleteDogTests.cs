//using Application.Commands.Dogs.DeleteDog;
//using Infrastructure.Database;
//using Infrastructure.RealDatabase;

//namespace Test.DogTests.CommandTests
//{
//    [TestFixture]
//    public class DeleteDogTests
//    {
//        private DeleteDogByIdCommandHandler _handler;
//        private MockDatabase _mockDatabase;
//        private MySqlDB _mySqlDb;

//        [SetUp]
//        public void SetUp()
//        {
//            //Initialize the handler and mock database before each test
//            _mockDatabase = new MockDatabase();
//            _mySqlDb = new MySqlDB();
//            _handler = new DeleteDogByIdCommandHandler(_mockDatabase, _mySqlDb);
//        }

//        [Test]
//        public async Task Handle_DeleteDogValidId_RemovesDogFromList()
//        {
//            // Arrange
//            var deleteDogCommand = new DeleteDogByIdCommand(new Guid("50d5bb3e-0f5b-45fe-9a28-3c801db763d1"));

//            // Act
//            var deletedDog = await _handler.Handle(deleteDogCommand, CancellationToken.None);
//            var dogListAfterDeletion = _mySqlDb.Dogs.ToList();

//            // Assert
//            Assert.NotNull(deletedDog);
//            Assert.That(dogListAfterDeletion, Does.Not.Contain(deletedDog));
//        }

//        [Test]
//        public async Task Handle_DeleteDogInvalidId_ReturnsNull()
//        {
//            // Arrange
//            var deleteDogCommand = new DeleteDogByIdCommand(Guid.NewGuid());

//            /// Act
//            var deletedDog = await _handler.Handle(deleteDogCommand, CancellationToken.None);

//            // Assert
//            Assert.Null(deletedDog);
//        }
//    }
//}