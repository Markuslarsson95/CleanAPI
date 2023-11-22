using Application.Commands.Cats;
using Application.Dtos;
using Application.Queries.Cats.GetAll;
using Infrastructure.Database;

namespace Test.CatTests.CommandTests
{
    [TestFixture]
    public class AddCatTests
    {
        private AddCatCommandHandler _handler;
        private GetAllCatsQueryHandler _allDogsHandler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            //Initialize the handler and mock database before each test
            _mockDatabase = new MockDatabase();
            _handler = new AddCatCommandHandler(_mockDatabase);
            _allDogsHandler = new GetAllCatsQueryHandler(_mockDatabase);
        }

        [Test]
        public async Task Handle_AddNewValidCat_ReturnsNewCat()
        {
            // Arrange
            var newCatDto = new CatDto { Name = "testNameCat" };
            var addCatCommand = new AddCatCommand(newCatDto);

            // Act
            var addedCat = await _handler.Handle(addCatCommand, CancellationToken.None);

            var getAllCatsQuery = new GetAllCatsQuery();
            var allCats = await _allDogsHandler.Handle(getAllCatsQuery, CancellationToken.None);

            // Assert
            Assert.NotNull(addedCat);
            Assert.Contains(addedCat, allCats);
        }
    }
}
