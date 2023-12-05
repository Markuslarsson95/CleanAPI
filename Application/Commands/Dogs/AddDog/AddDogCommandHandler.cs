﻿using Domain.Models;
using Domain.Repositories;
using Infrastructure.RealDatabase;
using MediatR;

namespace Application.Commands.Dogs
{
    public class AddDogCommandHandler : IRequestHandler<AddDogCommand, Dog>
    {
        private readonly MySqlDB _mySqlDb;
        private readonly IDogRepository _dogRepository;

        public AddDogCommandHandler(MySqlDB mySqlDb, IDogRepository dogRepository)
        {
            _mySqlDb = mySqlDb;
            _dogRepository = dogRepository;
        }

        public async Task<Dog> Handle(AddDogCommand request, CancellationToken cancellationToken)
        {
            Dog dogToCreate = new()
            {
                Id = Guid.NewGuid(),
                Name = request.NewDog.Name
            };
            _dogRepository.Add(dogToCreate);

            await _mySqlDb.SaveChangesAsync(cancellationToken);

            return Task.FromResult(dogToCreate).Result;
        }
    }
}
