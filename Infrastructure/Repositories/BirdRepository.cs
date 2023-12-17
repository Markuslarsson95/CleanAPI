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

        public async Task<List<Bird>> GetAll(string? sortByColor)
        {
            try
            {
                IQueryable<Bird> query = _mySqlDb.Birds.Include(x => x.Users).OrderByDescending(x => x.Name).ThenBy(x => x.Color);

                if (!string.IsNullOrEmpty(sortByColor))
                {
                    query = query.Where(x => x.Color == sortByColor);
                }

                var birdList = await query.ToListAsync();
                return birdList;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while getting bird list from the database", ex);
            }
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
    }
}
