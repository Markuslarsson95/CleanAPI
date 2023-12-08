using Application.Commands.Users.AddUser;
using FluentValidation;

namespace Application.Commands.Dogs.AddDog
{
    public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
    {
        public AddUserCommandValidator()
        {
            RuleFor(x => x.NewUser.UserName).NotEmpty().WithMessage("Username can not be empty or null")
            .MinimumLength(2).WithMessage("Username must be at least two characters long")
            .MaximumLength(30).WithMessage("Username can not be more than 30 characters long");
            RuleFor(x => x.NewUser.Password).NotEmpty().WithMessage("Password can not be empty or null")
                .MinimumLength(5).WithMessage("Password must be at least five characters long");
        }
    }
}
