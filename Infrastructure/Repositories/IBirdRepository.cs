using Domain.Models.Animals;

namespace Infrastructure.Repositories
{
    public interface IBirdRepository
    {
        Task<Bird?> GetById(Guid id);
        Task<List<Bird>> GetAll();
        Task<Bird> Add(Bird bird);
        Task<Bird> Update(Bird bird);
        Task<Bird> Delete(Bird bird);
        void Save();
    }
}
