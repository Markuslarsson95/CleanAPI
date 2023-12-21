using Application.Dtos;
using FluentValidation;

namespace Application.Validators.CatValidators
{
    public class CatValidator : AbstractValidator<CatDto>
    {
        public CatValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Cat name can not be empty or null")
                .MinimumLength(2).WithMessage("Cat name must be at least two characters long")
                .MaximumLength(30).WithMessage("Cat name can not be more than 30 characters long");
            RuleFor(x => x.LikesToPlay).NotNull().WithMessage("LikesToPlay must be true or false");
            RuleFor(x => x.Breed)
                .NotEmpty().WithMessage("Cat breed can not be empty or null")
                .MaximumLength(30).WithMessage("Cat breed must not exceed 30 charachters");
            RuleFor(x => x.Weight).GreaterThan(0).WithMessage("Cat weight must be higher than 0");
        }
    }
}
