﻿using FluentValidation;

namespace Application.Commands.Dogs.AddDog
{
    public class AddDogCommandValidator : AbstractValidator<AddDogCommand>
    {
        public AddDogCommandValidator()
        {
            RuleFor(x => x.NewDog.Name).NotEmpty().WithMessage("Name can not be empty or null")
            .MinimumLength(2).WithMessage("Name must be at least two characters long")
            .MaximumLength(30).WithMessage("Name can not be more than 30 characters long");
        }
    }
}
