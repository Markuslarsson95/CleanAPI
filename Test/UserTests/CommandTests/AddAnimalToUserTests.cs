using Application.Commands.Users.AddAnimalToUser;
using Application.Dtos;
using Domain.Models;
using Domain.Models.Animals;
using Infrastructure.Repositories.Animals;
using Infrastructure.Repositories.Users;
using Moq;

namespace Test.UserTests.CommandTests
{
    [TestFixture]
    public class AddAnimalToUserTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IAnimalRepository<Animal>> _animalRepositoryMock;
        private AddAnimalToUserCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _animalRepositoryMock = new Mock<IAnimalRepository<Animal>>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _handler = new AddAnimalToUserCommandHandler(_userRepositoryMock.Object, _animalRepositoryMock.Object);
        }

        [Test]
        public async Task Handle_ValidCommand_ReturnsUpdatedUser()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var animalIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
            var command = new AddAnimalToUserCommand(new AnimalUserDto { UserId = userId, AnimalId = animalIds });


            var existingUser = new User { Id = userId, Animals = new List<Animal>() };
            var existingAnimal = animalIds.Select(id => new Animal { Id = id }).ToList();

            _userRepositoryMock.Setup(x => x.GetById(userId)).ReturnsAsync(existingUser);
            _animalRepositoryMock.Setup(x => x.GetAnimalsByIds(animalIds)).ReturnsAsync(existingAnimal);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(userId, Is.EqualTo(result.Id));
            Assert.That(animalIds[0], Is.EqualTo(result.Animals[0].Id));
            _userRepositoryMock.Verify(x => x.GetById(userId), Times.Once);
            _animalRepositoryMock.Verify(x => x.GetAnimalsByIds(animalIds), Times.Once);
            _userRepositoryMock.Verify(x => x.Update(existingUser), Times.Once);
        }

        [Test]
        public async Task Handle_UserIsNull_ReturnsNull()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var animalIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
            var command = new AddAnimalToUserCommand(new AnimalUserDto { UserId = userId, AnimalId = animalIds });

            _userRepositoryMock.Setup(x => x.GetById(userId)).Returns(Task.FromResult((User?)null));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Null);
            _userRepositoryMock.Verify(x => x.GetById(userId), Times.Once);
            _animalRepositoryMock.Verify(x => x.GetAnimalById(It.IsAny<Guid>()), Times.Never);
            _userRepositoryMock.Verify(x => x.Update(It.IsAny<User>()), Times.Never);
        }
    }
}
