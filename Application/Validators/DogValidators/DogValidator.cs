using Application.Dtos;
using FluentValidation;

namespace Application.Validators.DogValidators
{
    public class DogValidator : AbstractValidator<DogDto>
    {
        public DogValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name can not be empty or null")
            .MinimumLength(2).WithMessage("Name must be at least two characters long")
            .MaximumLength(30).WithMessage("Name can not be more than 30 characters long");
            RuleFor(x => x.Breed).NotEmpty().WithMessage("Breed can not be empty or null");
            RuleFor(x => x.Weight).NotEmpty().WithMessage("Weight can not be empty or null").GreaterThan(0).WithMessage("Weight must be higher than 0");
        }
    }
}
