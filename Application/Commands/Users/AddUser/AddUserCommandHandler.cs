using Domain.Models;
using Infrastructure.Repositories;
using MediatR;

namespace Application.Commands.Users.AddUser
{
    public sealed class AddUserCommandHandler : IRequestHandler<AddUserCommand, User>
    {
        private readonly IUserRepository _userRepository;

        public AddUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            User userToCreate = new()
            {
                UserName = request.NewUser.UserName,
                Password = request.NewUser.Password
            };
            await _userRepository.Add(userToCreate);

            return await Task.FromResult(userToCreate);
        }
    }
}
