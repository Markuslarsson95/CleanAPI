﻿using Domain.Models;
using Domain.Repositories;
using MediatR;

namespace Application.Queries.Cats.GetById
{
    public class GetCatByIdQueryHandler : IRequestHandler<GetCatByIdQuery, Cat>
    {
        private readonly IGenericRepository<Cat> _catRepository;

        public GetCatByIdQueryHandler(IGenericRepository<Cat> catRepository)
        {
            _catRepository = catRepository;
        }

        public Task<Cat> Handle(GetCatByIdQuery request, CancellationToken cancellationToken)
        {
            Cat wantedCat = _catRepository.GetById(request.Id);

            if (wantedCat == null)
                return Task.FromResult<Cat>(null!);

            return Task.FromResult(wantedCat);
        }
    }
}
