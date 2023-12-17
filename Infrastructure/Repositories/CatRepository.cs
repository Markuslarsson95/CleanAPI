using Domain.Models.Animals;
using Infrastructure.RealDatabase;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CatRepository : ICatRepository
    {
        private readonly MySqlDB _mySqlDb;

        public CatRepository(MySqlDB mySqlDb)
        {
            _mySqlDb = mySqlDb;
        }

        public async Task<List<Cat>> GetAll(string? sortByBreed, int? sortByWeight)
        {
            try
            {
                IQueryable<Cat> query = _mySqlDb.Cats.Include(x => x.Users).OrderBy(x => x.Name).ThenBy(x => x.Breed);

                if (!string.IsNullOrEmpty(sortByBreed))
                {
                    query = query.Where(x => x.Breed == sortByBreed);
                }

                if (sortByWeight > 0)
                {
                    query = query.Where(x => x.Weight >= sortByWeight);
                }

                var catList = await query.ToListAsync();
                return catList;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while getting cat list from the database", ex);
            }

        }

        public async Task<Cat?> GetById(Guid id)
        {
            var cat = _mySqlDb.Cats
                .Include(x => x.Users)
                .FirstOrDefault(x => x.Id == id);
            return await Task.FromResult(cat);
        }

        public async Task<Cat> Add(Cat cat)
        {
            _mySqlDb.Cats.Add(cat);
            _mySqlDb.SaveChanges();
            return await Task.FromResult(cat);
        }

        public async Task<Cat> Update(Cat cat)
        {
            _mySqlDb.Cats.Update(cat);
            _mySqlDb.SaveChanges();
            return await Task.FromResult(cat);
        }

        public async Task<Cat> Delete(Cat cat)
        {
            _mySqlDb.Cats.Remove(cat);
            _mySqlDb.SaveChanges();
            return await Task.FromResult(cat);
        }
    }
}
