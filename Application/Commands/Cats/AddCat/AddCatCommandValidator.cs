using FluentValidation;

namespace Application.Commands.Cats.AddCat
{
    public class AddCatCommandValidator : AbstractValidator<AddCatCommand>
    {
        public AddCatCommandValidator()
        {
            RuleFor(x => x.NewCat.Name).NotEmpty().WithMessage("Name can not be empty or null")
                .MinimumLength(2).WithMessage("Name must be at least two characters long")
                .MaximumLength(15).WithMessage("Name can not be more than 15 characters long");
            RuleFor(x => x.NewCat.LikesToPlay).NotNull().WithMessage("LikesToPlay must be true or false");
        }
    }
}
