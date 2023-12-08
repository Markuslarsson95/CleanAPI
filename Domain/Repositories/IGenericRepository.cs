namespace Domain.Repositories
{
    public interface IGenericRepository<T>
    {
        T GetById(Guid id);
        IEnumerable<T> GetAll();
        Task<T> Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Save();
    }
}
