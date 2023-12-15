using Application.Dtos;
using FluentValidation;

namespace Application.Validators.BirdValidators
{
    public class BirdValidator : AbstractValidator<BirdDto>
    {
        public BirdValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name can not be empty or null")
                .MinimumLength(2).WithMessage("Name must be at least two characters long")
                .MaximumLength(30).WithMessage("Name can not be more than 30 characters long");
            RuleFor(x => x.CanFly).NotNull().WithMessage("CanFly must be true or false");
            RuleFor(x => x.Color).NotEmpty().WithMessage("Color can not be empty or null");
        }
    }
}
