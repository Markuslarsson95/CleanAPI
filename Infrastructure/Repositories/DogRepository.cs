using Domain.Models;
using Domain.Repositories;
using Infrastructure.RealDatabase;

namespace Infrastructure.Repositories
{
    public class DogRepository : IDogRepository
    {
        private readonly MySqlDB _mySqlDB;

        public DogRepository(MySqlDB mySqlDB)
        {
            _mySqlDB = mySqlDB;
        }

        public void Add(Dog dog)
        {
            _mySqlDB.Dogs.Add(dog);
        }

        public void Delete(Dog dog)
        {
            _mySqlDB.Dogs.Remove(dog);
        }

        public async Task<List<Dog>> GetAll()
        {
           var dogList = _mySqlDB.Dogs.ToList();
           return await Task.FromResult(dogList);
        }

        public async Task<Dog> GetById(Guid id)
        {
            var dog = _mySqlDB.Dogs.FirstOrDefault(d => d.Id == id);
            if (dog != null)
            {
                return dog;
            }
            else
                return await Task.FromResult<Dog>(null!);
        }
            
        public void Update(Dog dog)
        {
            _mySqlDB.Dogs.Update(dog);
        }
    }
}
