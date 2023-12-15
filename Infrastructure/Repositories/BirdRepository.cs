using Domain.Models.Animals;
using Infrastructure.RealDatabase;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BirdRepository : IBirdRepository
    {
        private readonly MySqlDB _mySqlDb;

        public BirdRepository(MySqlDB mySqlDb)
        {
            _mySqlDb = mySqlDb;
        }

        public async Task<List<Bird>> GetAll()
        {
            var birdList = _mySqlDb.Birds
                .Include(x => x.Users)
                .ToList();
            return await Task.FromResult(birdList);
        }

        public async Task<Bird?> GetById(Guid id)
        {
            var bird = _mySqlDb.Birds
                .Include(x => x.Users)
                .FirstOrDefault(x => x.Id == id);
            return await Task.FromResult(bird);
        }

        public async Task<Bird> Add(Bird bird)
        {
            _mySqlDb.Birds.Add(bird);
            _mySqlDb.SaveChanges();
            return await Task.FromResult(bird);
        }

        public async Task<Bird> Update(Bird bird)
        {
            _mySqlDb.Birds.Update(bird);
            _mySqlDb.SaveChanges();
            return await Task.FromResult(bird);
        }

        public async Task<Bird> Delete(Bird bird)
        {
            _mySqlDb.Birds.Remove(bird);
            _mySqlDb.SaveChanges();
            return await Task.FromResult(bird);
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
