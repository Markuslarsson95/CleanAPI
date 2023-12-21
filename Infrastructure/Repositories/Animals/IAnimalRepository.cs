using Domain.Models.Animals;

namespace Infrastructure.Repositories.Animals
{
    public interface IAnimalRepository<T> where T : Animal
    {
        Task<T> GetAnimalById(Guid id);
        Task<List<T>> GetAnimalsByIds(List<Guid> ids);
    }
}
