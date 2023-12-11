﻿using Application.Commands.Users.AddUser;
using Application.Dtos;
using Domain.Models;
using Domain.Repositories;
using Infrastructure.RealDatabase;
using Moq;

namespace Test.UserTests.CommandTests
{
    [TestFixture]
    public class AddUserTests
    {
        private Mock<IGenericRepository<User>> _userRepositoryMock;
        private Mock<MySqlDB> _mySqlDbMock = new Mock<MySqlDB>();
        private AddUserCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _mySqlDbMock.Setup(x => x.Add(It.IsAny<User>()));
            _mySqlDbMock.Setup(x => x.SaveChanges());
            _userRepositoryMock = new Mock<IGenericRepository<User>>();
            _handler = new AddUserCommandHandler(_userRepositoryMock.Object);
        }

        [Test]
        public async Task Handle_Should_AddNewUser_WhenValid()
        {
            // Arrange
            var userCommand = new AddUserCommand(new UserDto { UserName = "User", Password = "Password" });

            // Act
            var result = await _handler.Handle(userCommand, default);

            // Assert
            Assert.That(result, Is.Not.Null);
            _userRepositoryMock.Verify(x => x.Add(It.Is<User>(u => u.Id == result.Id
            && u.UserName == result.UserName && u.Password == result.Password)), Times.Once);
            _userRepositoryMock.Verify(x => x.Save(), Times.Once);
        }
    }
}
