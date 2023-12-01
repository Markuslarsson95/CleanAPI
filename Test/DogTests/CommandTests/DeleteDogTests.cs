using Application.Commands.Dogs.DeleteDog;
using Infrastructure.Database;
using Infrastructure.RealDatabase;

namespace Test.DogTests.CommandTests
{
    [TestFixture]
    public class DeleteDogTests
    {
        private DeleteDogByIdCommandHandler _handler;
        private MockDatabase _mockDatabase;
        private MySqlDB _mySqlDb;

        [SetUp]
        public void SetUp()
        {
            //Initialize the handler and mock database before each test
            _mockDatabase = new MockDatabase();
            _handler = new DeleteDogByIdCommandHandler(_mockDatabase, _mySqlDb);
        }

        [Test]
        public async Task Handle_DeleteDogValidId_RemovesDogFromList()
        {
            // Arrange
            var deleteDogCommand = new DeleteDogByIdCommand(new Guid("12345678-1234-5678-1234-573295756761"));

            // Act
            var deletedDog = await _handler.Handle(deleteDogCommand, CancellationToken.None);
            var dogListAfterDeletion = _mockDatabase.Dogs;

            // Assert
            Assert.NotNull(deletedDog);
            Assert.That(dogListAfterDeletion, Does.Not.Contain(deletedDog));
        }

        [Test]
        public async Task Handle_DeleteDogInvalidId_ReturnsNull()
        {
            // Arrange
            var deleteDogCommand = new DeleteDogByIdCommand(Guid.NewGuid());

            /// Act
            var deletedDog = await _handler.Handle(deleteDogCommand, CancellationToken.None);

            // Assert
            Assert.Null(deletedDog);
        }
    }
}