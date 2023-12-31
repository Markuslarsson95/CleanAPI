﻿using Domain.Models.Animals;
using MediatR;

namespace Application.Queries.Birds
{
    public class GetBirdByIdQuery : IRequest<Bird>
    {
        public GetBirdByIdQuery(Guid birdId)
        {
            Id = birdId;
        }

        public Guid Id { get; }
    }
}
