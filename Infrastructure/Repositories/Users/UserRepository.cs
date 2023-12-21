using Domain.Models;
using Infrastructure.RealDatabase;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly SqlDbContext _sqlDbContext;

        public UserRepository(SqlDbContext sqlDbContext)
        {
            _sqlDbContext = sqlDbContext;
        }

        public async Task<List<User>> GetAll()
        {
            try
            {
                IQueryable<User> query = _sqlDbContext.Users.Include(x => x.Animals).OrderBy(x => x.UserName);

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
            var user = _sqlDbContext.Users
                .Include(x => x.Animals)
                .FirstOrDefault(x => x.Id == id);
            return await Task.FromResult(user);
        }

        public async Task<User> Add(User user)
        {
            _sqlDbContext.Users.Add(user);
            _sqlDbContext.SaveChanges();
            return await Task.FromResult(user);
        }

        public async Task<User> Update(User user)
        {
            _sqlDbContext.Users.Update(user);
            _sqlDbContext.SaveChanges();
            return await Task.FromResult(user);
        }

        public async Task<User> Delete(User user)
        {
            _sqlDbContext.Users.Remove(user);
            _sqlDbContext.SaveChanges();
            return await Task.FromResult(user);
        }
    }
}
