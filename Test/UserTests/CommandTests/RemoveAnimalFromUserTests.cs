using Application.Commands.Users.RemoveAnimalFromUser;
using Domain.Models.Animals;
using Domain.Models;
using Moq;
using Application.Dtos;
using Infrastructure.Repositories.Users;
using Infrastructure.Repositories.Animals;

namespace Test.UserTests.CommandTests
{
    [TestFixture]
    public class RemoveAnimalFromUserTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IAnimalRepository<Animal>> _animalRepositoryMock;
        private RemoveAnimalFromUserCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _animalRepositoryMock = new Mock<IAnimalRepository<Animal>>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _handler = new RemoveAnimalFromUserCommandHandler(_userRepositoryMock.Object, _animalRepositoryMock.Object);
        }

        [Test]
        public async Task Handle_ValidCommand_RemovesAnimalFromUser()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var animalId = Guid.NewGuid();
            var command = new RemoveAnimalFromUserCommand(new AnimalUserDto { UserId = userId, AnimalId = animalId });


            var existingUser = new User { Id = userId, Animals = new List<Animal> { new Animal { Id = animalId } } };

            _userRepositoryMock.Setup(repo => repo.GetById(userId)).ReturnsAsync(existingUser);
            _animalRepositoryMock.Setup(repo => repo.GetAnimalById(animalId)).ReturnsAsync(new Animal { Id = animalId });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(userId, Is.EqualTo(result.Id));
            _userRepositoryMock.Verify(repo => repo.GetById(userId), Times.Once);
            _animalRepositoryMock.Verify(repo => repo.GetAnimalById(animalId), Times.Once);
            _userRepositoryMock.Verify(repo => repo.Update(existingUser), Times.Once);
        }

        [Test]
        public async Task Handle_UserNotFound_ReturnsNull()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var animalId = Guid.NewGuid();
            var command = new RemoveAnimalFromUserCommand(new AnimalUserDto { UserId = userId, AnimalId = animalId });

            _userRepositoryMock.Setup(repo => repo.GetById(userId)).Returns(Task.FromResult((User?)null));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Null);
            _userRepositoryMock.Verify(repo => repo.GetById(userId), Times.Once);
            _animalRepositoryMock.Verify(repo => repo.GetAnimalById(It.IsAny<Guid>()), Times.Never);
            _userRepositoryMock.Verify(repo => repo.Update(It.IsAny<User>()), Times.Never);
        }

        [Test]
        public async Task Handle_AnimalNotFound_ReturnsUserWithoutChanges()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var animalId = Guid.NewGuid();
            var command = new RemoveAnimalFromUserCommand(new AnimalUserDto { UserId = userId, AnimalId = animalId });

            var existingUser = new User { Id = userId, Password = "password", Animals = new List<Animal> { new Dog { Id = Guid.NewGuid() } } };

            _userRepositoryMock.Setup(repo => repo.GetById(userId)).ReturnsAsync(existingUser);
            _animalRepositoryMock.Setup(x => x.GetAnimalById(animalId)).Returns(Task.FromResult((Animal?)null!));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Null);
            _userRepositoryMock.Verify(repo => repo.GetById(userId), Times.Once);
            _animalRepositoryMock.Verify(repo => repo.GetAnimalById(animalId), Times.Once);
            _userRepositoryMock.Verify(repo => repo.Update(It.IsAny<User>()), Times.Never);
        }
    }
}
