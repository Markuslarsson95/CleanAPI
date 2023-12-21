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
            var animalId = Guid.NewGuid();
            var command = new AddAnimalToUserCommand(new AnimalUserDto { UserId = userId, AnimalId = animalId });


            var existingUser = new User { Id = userId, Animals = new List<Animal>() };
            var existingAnimal = new Animal { Id = animalId };

            _userRepositoryMock.Setup(x => x.GetById(userId)).ReturnsAsync(existingUser);
            _animalRepositoryMock.Setup(x => x.GetAnimalById(animalId)).ReturnsAsync(existingAnimal);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(userId, Is.EqualTo(result.Id));
            Assert.That(animalId, Is.EqualTo(result.Animals[0].Id));
            _userRepositoryMock.Verify(x => x.GetById(userId), Times.Once);
            _animalRepositoryMock.Verify(x => x.GetAnimalById(animalId), Times.Once);
            _userRepositoryMock.Verify(x => x.Update(existingUser), Times.Once);
        }

        [Test]
        public async Task Handle_UserIsNull_ReturnsNull()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var animalId = Guid.NewGuid();
            var command = new AddAnimalToUserCommand(new AnimalUserDto { UserId = userId, AnimalId = animalId });

            _userRepositoryMock.Setup(x => x.GetById(userId)).Returns(Task.FromResult((User?)null));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Null);
            _userRepositoryMock.Verify(x => x.GetById(userId), Times.Once);
            _animalRepositoryMock.Verify(x => x.GetAnimalById(It.IsAny<Guid>()), Times.Never);
            _userRepositoryMock.Verify(x => x.Update(It.IsAny<User>()), Times.Never);
        }

        [Test]
        public async Task Handle_AnimalIsNull_ReturnsNull()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var animalId = Guid.NewGuid();
            var command = new AddAnimalToUserCommand(new AnimalUserDto { UserId = userId, AnimalId = animalId });

            var existingUser = new User { Id = userId, Animals = new List<Animal>() };

            _userRepositoryMock.Setup(x => x.GetById(userId)).ReturnsAsync(existingUser);
            _animalRepositoryMock.Setup(x => x.GetAnimalById(animalId)).Returns(Task.FromResult((Animal?)null!));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Null);
            _userRepositoryMock.Verify(x => x.GetById(userId), Times.Once);
            _animalRepositoryMock.Verify(x => x.GetAnimalById(animalId), Times.Once);
            _userRepositoryMock.Verify(x => x.Update(It.IsAny<User>()), Times.Never);
        }
    }
}
