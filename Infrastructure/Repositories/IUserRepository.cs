using Domain.Models;

namespace Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetById(Guid id);
        Task<List<User>> GetAll();
        Task<User> Add(User user);
        Task<User> Update(User user);
        Task<User> Delete(User user);
    }
}
