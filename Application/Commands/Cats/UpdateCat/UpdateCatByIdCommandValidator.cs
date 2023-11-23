using FluentValidation;

namespace Application.Commands.Cats.UpdateCat
{
    public class UpdateCatByIdCommandValidator : AbstractValidator<UpdateCatByIdCommand>
    {
        public UpdateCatByIdCommandValidator()
        {
            RuleFor(x => x.UpdatedCat.Name).NotEmpty().WithMessage("Name can not be empty or null")
                .MinimumLength(2).WithMessage("Name must be at least two characters long")
                .MaximumLength(15).WithMessage("Name can not be more than 15 characters long");
            RuleFor(x => x.UpdatedCat.LikesToPlay).NotNull().WithMessage("LikesToPlay must be true or false");
        }
    }
}
