using Domain.Models;
using Domain.Repositories;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private IGenericRepository<User> _repository;

        public UserRepository(IGenericRepository<User> repository)
        {
            _repository = repository;
        }
        public bool BeUniqueUsername(string username)
        {
            // Check if the username is unique in the database
            List<User> allUsersFromDb = _repository.GetAll().Result;

            return !allUsersFromDb.Any(user => user.UserName.ToLower() == username.ToLower());
        }
    }
}
