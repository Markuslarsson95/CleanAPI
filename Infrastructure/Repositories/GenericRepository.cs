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

        public T GetById(Guid id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public async Task<T> Add(T entity)
        {
            _dbSet.Add(entity);
            return await Task.FromResult(entity);
        }

        public void Update(T entity)
        {
            _mySqlDb.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void Save()
        {
            _mySqlDb.SaveChanges();
        }
    }
}
