using Application.Dtos;
using FluentValidation;

namespace Application.Validators.DogValidators
{
    public class DogValidator : AbstractValidator<DogDto>
    {
        public DogValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Dog name can not be empty or null")
            .MinimumLength(2).WithMessage("Dog name must be at least two characters long")
            .MaximumLength(30).WithMessage("Dog name can not be more than 30 characters long");
            RuleFor(x => x.Breed)
                .NotEmpty().WithMessage("Dog breed can not be empty or null")
                .MaximumLength(30).WithMessage("Dog breed must not exceed 30 charachters");
            RuleFor(x => x.Weight).GreaterThan(0).WithMessage("Dog weight must be higher than 0");
        }
    }
}
