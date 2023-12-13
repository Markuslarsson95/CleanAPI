using Domain.Models;
using Infrastructure.RealDatabase;

namespace Infrastructure.Repositories
{
    public class CatRepository : ICatRepository
    {
        private readonly MySqlDB _mySqlDb;

        public CatRepository(MySqlDB mySqlDb)
        {
            _mySqlDb = mySqlDb;
        }

        public async Task<List<Cat>> GetAll()
        {
            var catList = _mySqlDb.Cats.ToList();
            return await Task.FromResult(catList);
        }

        public async Task<Cat?> GetById(Guid id)
        {
            var cat = _mySqlDb.Cats.Find(id);
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

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
