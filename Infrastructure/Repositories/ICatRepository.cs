using Domain.Models.Animals;

namespace Infrastructure.Repositories
{
    public interface ICatRepository
    {
        Task<Cat?> GetById(Guid id);
        Task<List<Cat>> GetAll(string? sortByBreed, int? sortByWeight);
        Task<Cat> Add(Cat cat);
        Task<Cat> Update(Cat cat);
        Task<Cat> Delete(Cat cat);
    }
}
