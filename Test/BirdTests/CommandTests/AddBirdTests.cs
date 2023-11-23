using Application.Commands.Birds;
using Application.Dtos;
using Application.Queries.Birds.GetAll;
using Infrastructure.Database;

namespace Test.BirdTests.CommandTests
{
    [TestFixture]
    public class AddBirdTests
    {
        private AddBirdCommandHandler _handler;
        private GetAllBirdsQueryHandler _allBirdsHandler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            //Initialize the handler and mock database before each test
            _mockDatabase = new MockDatabase();
            _handler = new AddBirdCommandHandler(_mockDatabase);
            _allBirdsHandler = new GetAllBirdsQueryHandler(_mockDatabase);
        }

        [Test]
        public async Task Handle_AddNewValidBird_ReturnsNewBirdList()
        {
            // Arrange
            var newBirdDto = new BirdDto { Name = "testNameBird", CanFly = true };
            var addBirdCommand = new AddBirdCommand(newBirdDto);

            // Act
            var addedBird = await _handler.Handle(addBirdCommand, CancellationToken.None);

            var getAllBirdsQuery = new GetAllBirdsQuery();
            var allBirds = await _allBirdsHandler.Handle(getAllBirdsQuery, CancellationToken.None);

            // Assert
            Assert.NotNull(addedBird);
            Assert.Contains(addedBird, allBirds);
        }
    }
}
