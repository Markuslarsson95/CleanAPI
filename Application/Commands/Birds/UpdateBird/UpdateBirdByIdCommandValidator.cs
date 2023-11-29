using FluentValidation;

namespace Application.Commands.Birds.UpdateBird
{
    public class UpdateBirdByIdCommandValidator : AbstractValidator<UpdateBirdByIdCommand>
    {
        public UpdateBirdByIdCommandValidator()
        {
            RuleFor(x => x.UpdatedBird.Name).NotEmpty().WithMessage("Name can not be empty or null")
                .MinimumLength(2).WithMessage("Name must be at least two characters long")
                .MaximumLength(30).WithMessage("Name can not be more than 30 characters long");
            RuleFor(x => x.UpdatedBird.CanFly).NotNull().WithMessage("CanFly must be true or false");
        }
    }
}
