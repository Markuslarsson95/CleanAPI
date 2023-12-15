using Domain.Models;
using Domain.Models.Animals;
using Infrastructure.RealDatabase;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class DogRepository : IDogRepository
    {
        private readonly MySqlDB _mySqlDb;

        public DogRepository(MySqlDB mySqlDb)
        {
            _mySqlDb = mySqlDb;
        }

        public async Task<List<Dog>> GetAll()
        {
            var dogList = _mySqlDb.Dogs
                .Include(x => x.Users)
                .ToList();
            return await Task.FromResult(dogList);
        }

        public async Task<Dog?> GetById(Guid id)
        {
            var dog = _mySqlDb.Dogs
                .Include(x => x.Users)
                .FirstOrDefault(x => x.Id == id);
            return await Task.FromResult(dog);
        }

        public async Task<Dog> Add(Dog dog)
        {
            _mySqlDb.Dogs.Add(dog);
            _mySqlDb.SaveChanges();
            return await Task.FromResult(dog);
        }

        public async Task<Dog> Update(Dog dog)
        {
            _mySqlDb.Dogs.Update(dog);
            _mySqlDb.SaveChanges();
            return await Task.FromResult(dog);
        }

        public async Task<Dog> Delete(Dog dog)
        {
            _mySqlDb.Dogs.Remove(dog);
            _mySqlDb.SaveChanges();
            return await Task.FromResult(dog);
        }

        public void Save()
        {
            throw new NotImplementedException();
        }


    }
}
