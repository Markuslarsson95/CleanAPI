using Domain.Repositories;
using Infrastructure.RealDatabase;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly MySqlDB _mySqlDb;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(MySqlDB mySqlDb)
        {
            _mySqlDb = mySqlDb;
            _dbSet = _mySqlDb.Set<T>();
        }

        public async Task<T> GetById(Guid id)
        {
            try
            {
                var wantedObject = _dbSet.Find(id);
                return await Task.FromResult(wantedObject!);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occured while getting a {typeof(T).Name} with Id {id} from the database", ex);
            }
        }

        public async Task<List<T>> GetAll()
        {
            var objectList = _dbSet.ToList();
            return await Task.FromResult(objectList);
        }

        public async Task<T> Add(T entity)
        {
            _dbSet.Add(entity);
            return await Task.FromResult(entity);
        }

        public async Task<T> Update(T entity)
        {
            _dbSet.Update(entity);
            return await Task.FromResult(entity);
        }

        public async Task<T> Delete(T entity)
        {
            _dbSet.Remove(entity);
            return await Task.FromResult(entity);
        }

        public void Save()
        {
            _mySqlDb.SaveChanges();
        }
    }
}
