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

        public async Task<List<Dog>> GetAll(string? sortByBreed, int? sortByWeight)
        {
            try
            {
                IQueryable<Dog> query = _mySqlDb.Dogs.Include(x => x.Users).OrderBy(x => x.Name).ThenBy(x => x.Breed);

                if (!string.IsNullOrEmpty(sortByBreed))
                {
                    query = query.Where(x => x.Breed == sortByBreed);
                }

                if (sortByWeight > 0)
                {
                    query = query.Where(x => x.Weight >= sortByWeight);
                }

                var dogList = await query.ToListAsync();
                return dogList;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while getting dog list from the database", ex);
            }
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
    }
}
