using AcuCall.Core.Objects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcuCall.Core.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int userId);
        Task<User> FindUserByCredentialsAsync(string username, string password);
        Task<int> AddUserAsync(User user);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int id);
    }
}
