using Application.Commands.Cats;
using Application.Dtos;
using Domain.Models;
using Domain.Repositories;
using Moq;

namespace Test.CatTests.CommandTests
{
    [TestFixture]
    public class AddCatTests
    {
        private Mock<IGenericRepository<Cat>> _catRepositoryMock;
        private AddCatCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            //Initialize the handler and mock database before each test
            _catRepositoryMock = new Mock<IGenericRepository<Cat>>();
            _handler = new AddCatCommandHandler(_catRepositoryMock.Object);
        }

        [Test]
        public async Task Handle_Should_AddNewCat_WhenValid()
        {
            // Arrange
            var addCatCommand = new AddCatCommand(new CatDto { Name = "Test", LikesToPlay = true });

            _catRepositoryMock.Setup(x => x.Add(
                It.IsAny<Cat>()));

            // Act
            var result = await _handler.Handle(addCatCommand, default);

            // Assert
            Assert.That(result, Is.Not.Null);
            _catRepositoryMock.Verify(x => x.Add(It.Is<Cat>(d => d.Id == result.Id)), Times.Once);
            _catRepositoryMock.Verify(x => x.Save(), Times.Once);
        }
    }
}
