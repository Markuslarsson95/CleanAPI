using Domain.Models.Animals;
using Infrastructure.RealDatabase;
using Microsoft.EntityFrameworkCore;
using Serilog;


namespace Infrastructure.Repositories.Cats
{
    public class CatRepository : ICatRepository
    {
        private readonly SqlDbContext _sqlDbContext;

        public CatRepository(SqlDbContext sqlDbContext)
        {
            _sqlDbContext = sqlDbContext;
        }

        public async Task<List<Cat>> GetAll(string? sortByBreed, int? sortByWeight)
        {
            try
            {
                Log.Information("Looking for Cats in database");

                IQueryable<Cat> query = _sqlDbContext.Cats.Include(x => x.Users).OrderBy(x => x.Name).ThenBy(x => x.Breed);

                if (!string.IsNullOrEmpty(sortByBreed))
                {
                    Log.Information("Filtering list by breed");
                    query = query.Where(x => x.Breed == sortByBreed);
                }

                if (sortByWeight > 0)
                {
                    Log.Information("Filtering list by weight");
                    query = query.Where(x => x.Weight >= sortByWeight);
                }

                var catList = await query.ToListAsync();
                return catList;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while getting cat list from the database");
                throw new Exception("Error occurred while getting cat list from the database", ex);
            }
        }

        public async Task<Cat?> GetById(Guid id)
        {
            try
            {
                Log.Information("Looking for Cat in database");

                var cat = _sqlDbContext.Cats
                    .Include(x => x.Users)
                    .FirstOrDefault(x => x.Id == id);

                return await Task.FromResult(cat);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while getting cat by Id");
                throw new Exception("Error occurred while getting cat by Id", ex);
            }
        }

        public async Task<Cat> Add(Cat cat)
        {
            try
            {
                Log.Information("Adding a new cat to the database");

                _sqlDbContext.Cats.Add(cat);
                _sqlDbContext.SaveChanges();

                Log.Information("Successfully added a new cat to the database");

                return await Task.FromResult(cat);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while adding a cat to the database");
                throw new Exception("Error occurred while adding a cat to the database", ex);
            }
        }

        public async Task<Cat> Update(Cat cat)
        {
            try
            {
                Log.Information("Updating cat in the database");

                _sqlDbContext.Cats.Update(cat);
                _sqlDbContext.SaveChanges();

                Log.Information("Successfully updated cat in the database");

                return await Task.FromResult(cat);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while updating a cat in the database");
                throw new Exception("Error occurred while updating a cat in the database", ex);
            }
        }

        public async Task<Cat> Delete(Cat cat)
        {
            try
            {
                Log.Information("Removing cat from the database");

                _sqlDbContext.Cats.Remove(cat);
                _sqlDbContext.SaveChanges();

                Log.Information("Successfully removed cat in the database");
                return await Task.FromResult(cat);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while removing a cat from the database");
                throw new Exception("Error occurred while removing a cat from the database", ex);
            }
        }
    }
}
