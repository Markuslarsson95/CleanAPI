﻿using Application.Dtos;
using Domain.Models.Animals;
using MediatR;

namespace Application.Commands.Cats
{
    public class UpdateCatByIdCommand : IRequest<Cat>
    {
        public UpdateCatByIdCommand(CatDto updatedCat, Guid id)
        {
            UpdatedCat = updatedCat;
            Id = id;
        }

        public CatDto UpdatedCat { get; }
        public Guid Id { get; }
    }
}
