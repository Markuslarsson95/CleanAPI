﻿using Domain.Models.Animal;

namespace Infrastructure.Repositories
{
    public interface IDogRepository
    {
        Task<Dog> GetById(Guid id);
        Task<List<Dog>> GetAll();
        Task<Dog> Add(Dog dog);
        Task<Dog> Update(Dog dog);
        Task<Dog> Delete(Dog dog);
        void Save();
    }
}
