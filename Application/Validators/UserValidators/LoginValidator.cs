using Application.Dtos;
using FluentValidation;

namespace Application.Validators.UserValidators
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username required!");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password required!");
        }
    }
}
