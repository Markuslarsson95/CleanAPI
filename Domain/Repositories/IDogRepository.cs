using Domain.Models;

namespace Domain.Repositories
{
    public interface IDogRepository
    {
        Task<List<Dog>> GetAll();
        Task<Dog> GetById(Guid id);
        void Add(Dog dog);
        void Update(Dog dog, string name);
        void Delete(Dog dog);
    }
}
