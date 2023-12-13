﻿using Domain.Models.Animal;
using MediatR;

namespace Application.Commands.Birds
{
    public class DeleteBirdByIdCommand : IRequest<Bird>
    {
        public DeleteBirdByIdCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
