using Domain.Models.Animals;
using Infrastructure.RealDatabase;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Animals
{
    public class AnimalRepository<T> : IAnimalRepository<T> where T : Animal
    {
        private readonly SqlDbContext _sqlDbContext;
        private readonly DbSet<T> _dbSet;

        public AnimalRepository(SqlDbContext sqlDbContext)
        {
            _sqlDbContext = sqlDbContext; ;
            _dbSet = _sqlDbContext.Set<T>();
        }
        public async Task<T> GetAnimalById(Guid id)
        {
            try
            {
                var wantedObject = _dbSet.FirstOrDefault(x => x.Id == id);
                if (wantedObject != null)
                {
                    return await Task.FromResult(wantedObject);
                }
                else
                {
                    throw new KeyNotFoundException($"No {typeof(T).Name} found with Id {id} in the database");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occured while getting a {typeof(T).Name} with Id {id} from the database", ex);
            }
        }
    }
}
