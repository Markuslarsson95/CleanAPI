﻿using Domain.Models;
using Infrastructure.Repositories.Users;
using MediatR;

namespace Application.Commands.Users.DeleteUser
{
    public sealed class DeleteUserByIdCommandHandler : IRequestHandler<DeleteUserByIdCommand, User>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserByIdCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Handle(DeleteUserByIdCommand request, CancellationToken cancellationToken)
        {
            var userToDelete = await _userRepository.GetById(request.Id);

            if (userToDelete == null)
                return await Task.FromResult<User>(null!);

            await _userRepository.Delete(userToDelete);

            return userToDelete;
        }
    }
}
