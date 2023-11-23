using Application.Commands.Cats;
using Application.Queries.Cats.GetAll;
using Infrastructure.Database;

namespace Test.CatTests.CommandTests
{
    [TestFixture]
    public class DeleteCatTests
    {
        private DeleteCatByIdCommandHandler _handler;
        private GetAllCatsQueryHandler _allCatsHandler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            //Initialize the handler and mock database before each test
            _mockDatabase = new MockDatabase();
            _handler = new DeleteCatByIdCommandHandler(_mockDatabase);
            _allCatsHandler = new GetAllCatsQueryHandler(_mockDatabase);
        }

        [Test]
        public async Task Handle_DeleteCatValidId_RemovesCatFromList()
        {
            // Arrange
            var catId = new Guid("12345678-1234-5678-1234-472756427786");
            var deleteCatCommand = new DeleteCatByIdCommand(catId);

            // Act
            var deletedCat = await _handler.Handle(deleteCatCommand, CancellationToken.None);
            var getAllCatsQuery = new GetAllCatsQuery();
            var catListAfterDeletion = await _allCatsHandler.Handle(getAllCatsQuery, CancellationToken.None);

            // Assert
            Assert.NotNull(deletedCat);
            Assert.That(catListAfterDeletion, Does.Not.Contain(deletedCat));
        }

        [Test]
        public async Task Handle_DeleteCatInvalidId_ReturnsNull()
        {
            // Arrange
            var invalidCatId = Guid.NewGuid();
            var deleteCatCommand = new DeleteCatByIdCommand(invalidCatId);

            /// Act
            var deletedCat = await _handler.Handle(deleteCatCommand, CancellationToken.None);

            // Assert
            Assert.Null(deletedCat);
        }
    }
}
