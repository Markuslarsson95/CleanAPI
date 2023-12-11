using Domain.Models;
using Domain.Repositories;
using MediatR;

namespace Application.Commands.Users.DeleteUser
{
    public sealed class DeleteUserByIdCommandHandler : IRequestHandler<DeleteUserByIdCommand, User>
    {
        private readonly IGenericRepository<User> _userRepository;

        public DeleteUserByIdCommandHandler(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Handle(DeleteUserByIdCommand request, CancellationToken cancellationToken)
        {
            var userToDelete = await _userRepository.GetById(request.Id);

            if (userToDelete == null)
                return await Task.FromResult<User>(null!);

            await _userRepository.Delete(userToDelete);

            _userRepository.Save();

            return userToDelete;
        }
    }
}
