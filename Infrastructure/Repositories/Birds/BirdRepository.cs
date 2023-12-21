using Domain.Models.Animals;
using Infrastructure.RealDatabase;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Infrastructure.Repositories.Birds
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
                Log.Information("Looking for Birds in the database");

                IQueryable<Bird> query = _mySqlDb.Birds.Include(x => x.Users).OrderByDescending(x => x.Name).ThenBy(x => x.Color);

                if (!string.IsNullOrEmpty(sortByColor))
                {
                    Log.Information("Filtering list by color");
                    query = query.Where(x => x.Color == sortByColor);
                }

                var birdList = await query.ToListAsync();

                Log.Information("Successfully retrieved Birds from the database");

                return birdList;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while getting bird list from the database");
                throw new Exception("Error occurred while getting bird list from the database", ex);
            }
        }

        public async Task<Bird?> GetById(Guid id)
        {
            try
            {
                Log.Information($"Looking for Bird with ID {id} in the database");

                var bird = _mySqlDb.Birds
                    .Include(x => x.Users)
                    .FirstOrDefault(x => x.Id == id);

                return await Task.FromResult(bird);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"An error occurred while getting Bird by ID {id}");
                throw new Exception($"An error occurred while getting Bird by ID {id}", ex);
            }
        }

        public async Task<Bird> Add(Bird bird)
        {
            try
            {
                Log.Information("Adding a new Bird to the database");

                _mySqlDb.Birds.Add(bird);
                _mySqlDb.SaveChanges();

                Log.Information("Successfully added a new Bird to the database");

                return await Task.FromResult(bird);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while adding a Bird to the database");
                throw new Exception("An error occurred while adding a Bird to the database", ex);
            }
        }

        public async Task<Bird> Update(Bird bird)
        {
            try
            {
                Log.Information("Updating Bird in the database");

                _mySqlDb.Birds.Update(bird);
                _mySqlDb.SaveChanges();

                Log.Information("Successfully updated Bird in the database");

                return await Task.FromResult(bird);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while updating a Bird in the database");
                throw new Exception("An error occurred while updating a Bird in the database", ex);
            }
        }

        public async Task<Bird> Delete(Bird bird)
        {
            try
            {
                Log.Information("Removing Bird from the database");

                _mySqlDb.Birds.Remove(bird);
                _mySqlDb.SaveChanges();

                Log.Information("Successfully removed Bird from the database");

                return await Task.FromResult(bird);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while removing a Bird from the database");
                throw new Exception("An error occurred while removing a Bird from the database", ex);
            }
        }
    }
}
