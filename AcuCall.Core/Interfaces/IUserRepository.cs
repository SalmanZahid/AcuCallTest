using AcuCall.Core.Objects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcuCall.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User> FindUserByCredentialsAsync(string username, string password);
        Task<int> AddUserAsync(User user);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int id);
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int userId);
    }
}
