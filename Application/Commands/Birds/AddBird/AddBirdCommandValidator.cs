using FluentValidation;

namespace Application.Commands.Birds.AddBird
{
    public class AddBirdCommandValidator : AbstractValidator<AddBirdCommand>
    {
        public AddBirdCommandValidator()
        {
            RuleFor(x => x.NewBird.Name).NotEmpty().WithMessage("Name can not be empty or null")
                .MinimumLength(2).WithMessage("Name must be at least two characters long")
                .MaximumLength(15).WithMessage("Name can not be more than 15 characters long");
            RuleFor(x => x.NewBird.CanFly).NotNull().WithMessage("CanFly must be true or false");
        }
    }
}
