using Application.Commands.Cats;
using Domain.Models;
using Domain.Repositories;
using Moq;

namespace Test.CatTests.CommandTests
{
    [TestFixture]
    public class DeleteCatTests
    {
        private Mock<IGenericRepository<Cat>> _catRepositoryMock;
        private DeleteCatByIdCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            //Initialize the handler and mock database before each test
            _catRepositoryMock = new Mock<IGenericRepository<Cat>>();
            _handler = new DeleteCatByIdCommandHandler(_catRepositoryMock.Object);
        }

        [Test]
        public async Task Handle_Should_DeleteCat_WhenIdIsValid()
        {
            // Arrange
            var command = new DeleteCatByIdCommand(Guid.NewGuid());

            _catRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(new Cat { Id = Guid.NewGuid(), Name = "Test", LikesToPlay = true });
            _catRepositoryMock.Setup(x => x.Delete(It.IsAny<Cat>()));

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            Assert.That(result, Is.Not.Null);
            _catRepositoryMock.Verify(x => x.Delete(It.Is<Cat>(d => d.Id == result.Id)), Times.Once);
            _catRepositoryMock.Verify(x => x.Save(), Times.Once);
        }

        [Test]
        public async Task Handle_Should_Not_DeleteCat_WhenIdIsNotValid()
        {
            // Arrange
            var command = new DeleteCatByIdCommand(Guid.NewGuid());

            _catRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync((Cat)null!);
            _catRepositoryMock.Setup(x => x.Delete(It.IsAny<Cat>()));

            /// Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Null);
            _catRepositoryMock.Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);
            _catRepositoryMock.Verify(x => x.Delete(It.IsAny<Cat>()), Times.Never);
            _catRepositoryMock.Verify(x => x.Save(), Times.Never);
        }
    }
}