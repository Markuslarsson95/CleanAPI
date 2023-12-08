namespace Domain.Repositories
{
    public interface IGenericRepository<T>
    {
        Task<T> GetById(Guid id);
        Task<List<T>> GetAll();
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(T entity);
        void Save();
    }
}
