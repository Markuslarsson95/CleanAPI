using Application.Commands.Cats;
using Application.Dtos;
using Domain.Models.Animal;
using Infrastructure.Repositories;
using Moq;

namespace Test.CatTests.CommandTests
{
    [TestFixture]
    public class UpdateCatTests
    {
        private Mock<ICatRepository> _catRepositoryMock;
        private UpdateCatByIdCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            //Initialize the handler and mock database before each test
            _catRepositoryMock = new Mock<ICatRepository>();
            _handler = new UpdateCatByIdCommandHandler(_catRepositoryMock.Object);
        }

        [Test]
        public async Task Handle_Should_UpdateCat_WhenIdIsValid()
        {
            // Arrange
            var command = new UpdateCatByIdCommand(new CatDto { Name = "Update", LikesToPlay = true }, Guid.NewGuid());

            _catRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(new Cat { Id = Guid.NewGuid(), Name = "Update", LikesToPlay = false });
            _catRepositoryMock.Setup(x => x.Update(It.IsAny<Cat>()));

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            Assert.That(result, Is.Not.Null);
            _catRepositoryMock.Verify(x => x.Update(It.Is<Cat>(d => d.Name == result.Name)), Times.Once);
            _catRepositoryMock.Verify(x => x.Update(It.Is<Cat>(d => d.Id == result.Id)), Times.Once);
        }

        [Test]
        public async Task Handle_Should_Not_UpdateCat_WhenIdIsNotValid()
        {
            // Arrange
            var command = new UpdateCatByIdCommand(new CatDto { Name = "Update", LikesToPlay = false }, Guid.NewGuid());

            _catRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync((Cat)null!);
            _catRepositoryMock.Setup(x => x.Update(It.IsAny<Cat>()));

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            Assert.That(result, Is.Null);
            _catRepositoryMock.Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);
            _catRepositoryMock.Verify(x => x.Update(It.IsAny<Cat>()), Times.Never);
        }
    }
}