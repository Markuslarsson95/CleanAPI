using Domain.Models.Animals;

namespace Infrastructure.Repositories
{
    public interface IDogRepository
    {
        Task<Dog?> GetById(Guid id);
        Task<List<Dog>> GetAll(string? sortByBreed, int? sortByWeight);
        Task<Dog> Add(Dog dog);
        Task<Dog> Update(Dog dog);
        Task<Dog> Delete(Dog dog);
    }
}
