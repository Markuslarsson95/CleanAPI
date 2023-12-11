﻿using Domain.Models;
using Domain.Repositories;
using FluentValidation;

namespace Application.Commands.Users.AddUser
{
    public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
    {
        private readonly IGenericRepository<User> _userRepository;
        public AddUserCommandValidator(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;

            RuleFor(x => x.NewUser.UserName).NotEmpty().WithMessage("Username can not be empty or null")
            .MinimumLength(2).WithMessage("Username must be at least two characters long")
            .MaximumLength(30).WithMessage("Username can not be more than 30 characters long")
            .Matches("^[a-zA-Z0-9_-]+$").WithMessage("Username can only contain letters, numbers, underscores, and hyphens.")
            .Must(BeUniqueUsername).WithMessage("Username is already taken.");
            RuleFor(x => x.NewUser.Password).NotEmpty().WithMessage("Password can not be empty or null")
                .MinimumLength(5).WithMessage("Password must be at least five characters long");
        }

        public bool BeUniqueUsername(string username)
        {
            List<User> allUsersFromDb = _userRepository.GetAll().Result;

            return !allUsersFromDb.Any(user => user.UserName.ToLower() == username.ToLower());
        }
    }
}
