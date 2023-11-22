using Application.Commands.Dogs.DeleteDog;
using Application.Queries.Dogs;
using Application.Queries.Dogs.GetAll;
using Infrastructure.Database;

namespace Test.DogTests.CommandTests
{
    [TestFixture]
    public class DeleteDogTests
    {
        private DeleteDogByIdCommandHandler _handler;
        private GetAllDogsQueryHandler _allDogsHandler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            //Initialize the handler and mock database before each test
            _mockDatabase = new MockDatabase();
            _handler = new DeleteDogByIdCommandHandler(_mockDatabase);
            _allDogsHandler = new GetAllDogsQueryHandler(_mockDatabase);
        }

        [Test]
        public async Task Handle_DeleteDogValidId_RemovesDogFromList()
        {
            // Arrange
            var dogId = new Guid("12345678-1234-5678-1234-573295756761");
            var deleteDogCommand = new DeleteDogByIdCommand(dogId);

            // Act
            var deletedDog = await _handler.Handle(deleteDogCommand, CancellationToken.None);
            var getAllDogsQuery = new GetAllDogsQuery();
            var dogListAfterDeletion = await _allDogsHandler.Handle(getAllDogsQuery, CancellationToken.None);

            // Assert
            Assert.NotNull(deletedDog);
            Assert.That(dogListAfterDeletion, Does.Not.Contain(deletedDog));
        }

        [Test]
        public async Task Handle_DeleteDogInvalidId_ReturnsNull()
        {
            // Arrange
            var invalidDogId = Guid.NewGuid();
            var deleteDogCommand = new DeleteDogByIdCommand(invalidDogId);

            /// Act
            var deletedDog = await _handler.Handle(deleteDogCommand, CancellationToken.None);

            // Assert
            Assert.Null(deletedDog);
        }
    }
}
