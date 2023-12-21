using Domain.Models;
using Infrastructure.Repositories.Password;
using Infrastructure.Repositories.Users;
using MediatR;

namespace Application.Commands.Users.UpdateUser
{
    public sealed class UpdateUserByIdCommandHandler : IRequestHandler<UpdateUserByIdCommand, User>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordEncryptor _passwordEncryptor;

        public UpdateUserByIdCommandHandler(IUserRepository userRepository, IPasswordEncryptor passwordEncryptor)
        {
            _userRepository = userRepository;
            _passwordEncryptor = passwordEncryptor;
        }
        public async Task<User> Handle(UpdateUserByIdCommand request, CancellationToken cancellationToken)
        {
            var userToUpdate = await _userRepository.GetById(request.Id);

            if (userToUpdate == null)
                return await Task.FromResult<User>(null!);

            userToUpdate.UserName = request.UpdatedUser.UserName;
            userToUpdate.Password = _passwordEncryptor.Encrypt(request.UpdatedUser.Password);
            await _userRepository.Update(userToUpdate);

            return userToUpdate;
        }
    }
}
