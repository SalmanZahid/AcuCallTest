using AcuCall.Core.Interfaces;
using AcuCall.Core.Objects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcuCall.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> AddUserAsync(User user)
        {            
            return await _repository.AddUserAsync(user);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            
            return await _repository.DeleteUserAsync(id);
        }

        public async Task<User> FindUserByCredentialsAsync(string username, string password)
        {
            return await _repository.FindUserByCredentialsAsync(username, password);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _repository.GetAllUsersAsync();
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _repository.GetUserByIdAsync(userId);
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            return await _repository.UpdateUserAsync(user);
        }
    }
}
