using Domain.Models;
using Domain.Repositories;
using MediatR;

namespace Application.Commands.Users.AddUser
{
    public sealed class AddUserCommandHandler : IRequestHandler<AddUserCommand, User>
    {
        private readonly IGenericRepository<User> _genericUserRepository;

        public AddUserCommandHandler(IGenericRepository<User> genericUserRepository)
        {
            _genericUserRepository = genericUserRepository;
        }

        public async Task<User> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            User userToCreate = new()
            {
                UserName = request.NewUser.UserName,
                Password = request.NewUser.Password
            };
            await _genericUserRepository.Add(userToCreate);

            _genericUserRepository.Save();

            return await Task.FromResult(userToCreate);
        }
    }
}
