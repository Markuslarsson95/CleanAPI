using Domain.Models.Animals;
using Infrastructure.RealDatabase;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Infrastructure.Repositories.Dogs
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
                Log.Information("Looking for Dogs in database");

                IQueryable<Dog> query = _mySqlDb.Dogs.Include(x => x.Users).OrderBy(x => x.Name).ThenBy(x => x.Breed);

                if (!string.IsNullOrEmpty(sortByBreed))
                {
                    Log.Information("Sorting list by breed");
                    query = query.Where(x => x.Breed == sortByBreed);
                }

                if (sortByWeight > 0)
                {
                    Log.Information("Sorting list by weight");
                    query = query.Where(x => x.Weight >= sortByWeight);
                }

                var dogList = await query.ToListAsync();
                return dogList;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while gettin list of dogs from database");

                throw new Exception("Error occured while getting dog list from the database", ex);
            }
        }

        public async Task<Dog?> GetById(Guid id)
        {
            try
            {
                Log.Information("Looking for Dog in database");

                var dog = _mySqlDb.Dogs
                    .Include(x => x.Users)
                    .FirstOrDefault(x => x.Id == id);

                return await Task.FromResult(dog);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while getting Dog by Id");
                throw new Exception(ex.Message);
            }
        }

        public async Task<Dog> Add(Dog dog)
        {
            try
            {
                Log.Information("Adding a new dog to the database");

                _mySqlDb.Dogs.Add(dog);
                _mySqlDb.SaveChanges();

                Log.Information("Successfully added a new dog to the database");

                return await Task.FromResult(dog);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while adding a dog to the database");
                throw new Exception(ex.Message);
            }
        }

        public async Task<Dog> Update(Dog dog)
        {
            try
            {
                Log.Information("Updating dog in the database");

                _mySqlDb.Dogs.Update(dog);
                _mySqlDb.SaveChanges();

                Log.Information("Successfully updated dog in the database");

                return await Task.FromResult(dog);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while updating a dog in the database");
                throw new Exception(ex.Message);
            }
        }

        public async Task<Dog> Delete(Dog dog)
        {
            try
            {
                Log.Information("Removing dog from the database");

                _mySqlDb.Dogs.Remove(dog);
                _mySqlDb.SaveChanges();

                Log.Information("Successfully remove dog in the database");
                return await Task.FromResult(dog);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while removing a dog in the database");
                throw new Exception(ex.Message);
            }
        }
    }
}
