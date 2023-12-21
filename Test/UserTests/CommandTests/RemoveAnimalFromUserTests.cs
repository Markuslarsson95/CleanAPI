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
            var animalIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
            var command = new RemoveAnimalFromUserCommand(new AnimalUserDto { UserId = userId, AnimalId = animalIds });


            var existingUser = new User { Id = userId, Animals = new List<Animal>() };
            var existingAnimal = animalIds.Select(id => new Animal { Id = id }).ToList();

            _userRepositoryMock.Setup(repo => repo.GetById(userId)).ReturnsAsync(existingUser);
            _animalRepositoryMock.Setup(repo => repo.GetAnimalsByIds(animalIds)).ReturnsAsync(existingAnimal);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(userId, Is.EqualTo(result.Id));
            _userRepositoryMock.Verify(repo => repo.GetById(userId), Times.Once);
            _animalRepositoryMock.Verify(repo => repo.GetAnimalsByIds(animalIds), Times.Once);
            _userRepositoryMock.Verify(repo => repo.Update(existingUser), Times.Once);
        }

        [Test]
        public async Task Handle_UserNotFound_ReturnsNull()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var animalIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
            var command = new RemoveAnimalFromUserCommand(new AnimalUserDto { UserId = userId, AnimalId = animalIds });

            _userRepositoryMock.Setup(repo => repo.GetById(userId)).Returns(Task.FromResult((User?)null));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Null);
            _userRepositoryMock.Verify(repo => repo.GetById(userId), Times.Once);
            _animalRepositoryMock.Verify(repo => repo.GetAnimalsByIds(It.IsAny<List<Guid>>()), Times.Never);
            _userRepositoryMock.Verify(repo => repo.Update(It.IsAny<User>()), Times.Never);
        }
    }
}
