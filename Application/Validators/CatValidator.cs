using Application.Dtos;
using Domain.Models;
using FluentValidation;

namespace Application.Validators
{
    public class CatValidator : AbstractValidator<CatDto>
    {
        public CatValidator() 
        {
            RuleFor(cat => cat.Name).NotEmpty();
            RuleFor(cat => cat.LikesToPlay)
                .Must(x => x == false || x == true)
                .WithMessage("LikesToPlay must be false or true");
        }
    }
}
