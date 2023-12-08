using Domain.Models;
using Domain.Repositories;
using MediatR;

namespace Application.Commands.Users.AddUser
{
    public sealed class AddUserCommandHandler : IRequestHandler<AddUserCommand, User>
    {
        private readonly IGenericRepository<User> _userRepository;

        public AddUserCommandHandler(IGenericRepository<User> userRepository)
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

            _userRepository.Save();

            return userToCreate;
        }
    }
}
