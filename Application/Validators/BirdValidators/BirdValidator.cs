using Application.Dtos;
using FluentValidation;

namespace Application.Validators.BirdValidators
{
    public class BirdValidator : AbstractValidator<BirdDto>
    {
        public BirdValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Bird name can not be empty or null")
                .MinimumLength(2).WithMessage("Bird name must be at least two characters long")
                .MaximumLength(30).WithMessage("Bird name can not be more than 30 characters long");
            RuleFor(x => x.CanFly).NotNull().WithMessage("CanFly must be true or false");
            RuleFor(bird => bird.Color)
            .NotEmpty().WithMessage("Bird color is required.")
            .MaximumLength(30).WithMessage("Bird color must not exceed 30 characters");
        }
    }
}
