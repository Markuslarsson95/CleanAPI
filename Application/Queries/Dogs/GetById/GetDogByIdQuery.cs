﻿using Domain.Models.Animals;
using MediatR;

namespace Application.Queries.Dogs.GetById
{
    public class GetDogByIdQuery : IRequest<Dog>
    {
        public GetDogByIdQuery(Guid dogId)
        {
            Id = dogId;
        }

        public Guid Id { get; }
    }
}
