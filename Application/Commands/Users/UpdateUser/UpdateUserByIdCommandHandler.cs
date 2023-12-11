using Domain.Models;
using Domain.Repositories;
using MediatR;

namespace Application.Commands.Users.UpdateUser
{
    public sealed class UpdateUserByIdCommandHandler : IRequestHandler<UpdateUserByIdCommand, User>
    {
        private readonly IGenericRepository<User> _genericUserRepository;

        public UpdateUserByIdCommandHandler(IGenericRepository<User> genericUserRepository)
        {
            _genericUserRepository = genericUserRepository;
        }
        public async Task<User> Handle(UpdateUserByIdCommand request, CancellationToken cancellationToken)
        {
            var userToUpdate = await _genericUserRepository.GetById(request.Id);

            if (userToUpdate == null)
                return await Task.FromResult<User>(null!);

            userToUpdate.UserName = request.UpdatedUser.UserName;
            userToUpdate.Password = request.UpdatedUser.Password;
            await _genericUserRepository.Update(userToUpdate);

            _genericUserRepository.Save();

            return userToUpdate;
        }
    }
}
