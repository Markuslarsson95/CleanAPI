using Domain.Models;
using Infrastructure.Database;
using MediatR;

namespace Application.Commands.Users.AddUser
{
    internal class AddUserCommandHandler : IRequestHandler<AddUserCommand, User>
    {
        private readonly MockDatabase _mockDatabase;

        public AddUserCommandHandler(MockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }

        public Task<User> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            User userToCreate = new()
            {
                UserName = request.NewUser.UserName,
                Password = request.NewUser.Password
            };

            _mockDatabase.Users.Add(userToCreate);

            return Task.FromResult(userToCreate);
        }
    }
}
