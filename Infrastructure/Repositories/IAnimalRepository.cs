using Domain.Models.Animals;

namespace Infrastructure.Repositories
{
    public interface IAnimalRepository<T> where T : Animal
    {
        Task<T> GetAnimalById(Guid id);
    }
}
