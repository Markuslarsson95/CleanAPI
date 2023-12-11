using Domain.Models;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        bool BeUniqueUsername(string username);
    }
}
