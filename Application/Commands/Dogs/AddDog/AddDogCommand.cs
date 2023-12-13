﻿using Application.Dtos;
using Domain.Models.Animal;
using MediatR;

namespace Application.Commands.Dogs
{
    public class AddDogCommand : IRequest<Dog>
    {
        public AddDogCommand(DogDto newDog)
        {
            NewDog = newDog;
        }

        public DogDto NewDog { get; }
    }
}
