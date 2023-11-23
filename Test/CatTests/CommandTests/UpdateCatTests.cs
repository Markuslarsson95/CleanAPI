using Application.Commands.Cats;
using Application.Dtos;
using Application.Queries.Cats.GetAll;
using Infrastructure.Database;

namespace Test.CatTests.CommandTests
{
    [TestFixture]
    public class UpdateCatTests
    {
        private UpdateCatByIdCommandHandler _handler;
        private GetAllCatsQueryHandler _allCatsHandler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            //Initialize the handler and mock database before each test
            _mockDatabase = new MockDatabase();
            _handler = new UpdateCatByIdCommandHandler(_mockDatabase);
            _allCatsHandler = new GetAllCatsQueryHandler(_mockDatabase);
        }

        [Test]
        public async Task Handle_UpdateCatValidId_ReturnsUpdatedCatList()
        {
            // Arrange
            var catId = new Guid("12345678-1234-5678-1234-677758277550");
            var updateCatDto = new CatDto()
            {
                Name = "TestUpdateCat",
                LikesToPlay = false
            };

            var query = new GetAllCatsQuery();
            var updateCatComand = new UpdateCatByIdCommand(updateCatDto, catId);

            // Act
            var updatedCat = await _handler.Handle(updateCatComand, CancellationToken.None);
            var catListAfterUpdate = await _allCatsHandler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(updatedCat);
            Assert.That(catListAfterUpdate, Does.Contain(updatedCat));
        }

        [Test]
        public async Task Handle_UpdateCatInvalidId_ReturnsNullReferenceException()
        {
            // Arrange
            var invalidCatId = Guid.NewGuid();
            var updateCatDto = new CatDto()
            {
                Name = "TestUpdateCat",
                LikesToPlay = false
            };

            var updateCatCommand = new UpdateCatByIdCommand(updateCatDto, invalidCatId);

            // Act & Assert
            Assert.ThrowsAsync<NullReferenceException>(
                async () => await _handler.Handle(updateCatCommand, CancellationToken.None));
        }
    }
}
