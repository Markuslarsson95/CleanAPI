using Domain.Models;
using Domain.Models.Animals;
using Infrastructure.RealDatabase;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MySqlDB _mySqlDb;

        public UserRepository(MySqlDB mySqlDb)
        {
            _mySqlDb = mySqlDb;
        }

        public async Task<List<User>> GetAll()
        {
            var userList = _mySqlDb.Users
                .Include(x => x.Animals)
                .ToList();
            return await Task.FromResult(userList);
        }

        public async Task<User?> GetById(Guid id)
        {
            var user = _mySqlDb.Users
                .Include(x => x.Animals)
                .FirstOrDefault(x => x.Id == id);
            return await Task.FromResult(user);
        }

        public async Task<User> Add(User user)
        {
            _mySqlDb.Users.Add(user);
            _mySqlDb.SaveChanges();
            return await Task.FromResult(user);
        }

        public async Task<User> Update(User user)
        {
            _mySqlDb.Users.Update(user);
            _mySqlDb.SaveChanges();
            return await Task.FromResult(user);
        }

        public async Task<User> Delete(User user)
        {
            _mySqlDb.Users.Remove(user);
            _mySqlDb.SaveChanges();
            return await Task.FromResult(user);
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
        //public bool BeUniqueUsername(string username)
        //{
        //    // Check if the username is unique in the database
        //    List<User> allUsersFromDb = _repository.GetAll().Result;

        //    return !allUsersFromDb.Any(user => user.UserName.ToLower() == username.ToLower());
        //}
    }
}
