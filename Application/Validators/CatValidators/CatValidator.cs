using Application.Dtos;
using FluentValidation;

namespace Application.Validators.CatValidators
{
    public class CatValidator : AbstractValidator<CatDto>
    {
        public CatValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name can not be empty or null")
                .MinimumLength(2).WithMessage("Name must be at least two characters long")
                .MaximumLength(30).WithMessage("Name can not be more than 30 characters long");
            RuleFor(x => x.LikesToPlay).NotNull().WithMessage("LikesToPlay must be true or false");
            RuleFor(x => x.Breed).NotEmpty().WithMessage("Breed can not be empty or null");
            RuleFor(x => x.Weight).NotEmpty().WithMessage("Weight can not be empty or null").GreaterThan(0).WithMessage("Weight must be higher than 0");
        }
    }
}
