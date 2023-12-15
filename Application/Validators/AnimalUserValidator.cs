using Application.Dtos;
using FluentValidation;

namespace Application.Validators
{
    public class AnimalUserValidator : AbstractValidator<AnimalUserDto>
    {
        public AnimalUserValidator()
        {
            RuleFor(x => x.UserId)
                .SetValidator(new GuidValidator());
            RuleFor(x => x.AnimalId)
                .SetValidator(new GuidValidator());
        }
    }
}
