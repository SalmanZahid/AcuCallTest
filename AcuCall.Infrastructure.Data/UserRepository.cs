using AcuCall.Core.Interfaces;
using AcuCall.Core.Objects;
using AcuCall.Infrastructure.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcuCall.Infrastructure.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly IAcuCallsContext _dbContext;

        public UserRepository(Interfaces.IAcuCallsContext context)
        {
            _dbContext = context;
        }

        public async Task<int> AddUserAsync(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return user.UserId;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);

            if (user == null)
                return false;

            _dbContext.Users.Remove(user);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<User> FindUserByCredentialsAsync(string username, string password)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Username == username && x.Password == password);
            return user;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _dbContext.Users.FindAsync(userId);
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            _dbContext.Users.Update(user);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
