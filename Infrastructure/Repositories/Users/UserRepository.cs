using Domain.Models;
using Infrastructure.RealDatabase;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Users
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
            try
            {
                IQueryable<User> query = _mySqlDb.Users.Include(x => x.Animals).OrderBy(x => x.UserName);

                var userList = await query.ToListAsync();
                return userList;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while getting user list from the database", ex);
            }
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
    }
}
