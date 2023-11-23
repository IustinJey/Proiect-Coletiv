using System;
using System.Threading.Tasks;
using skillz_backend.data;
using skillz_backend.models;
using skillz_backend.Repositories;

namespace skillz_backend.Services
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(int userId);
        Task<User> GetUserByUsernameAsync(string username);
        Task<User> GetUserByEmailAsync(string email);
        Task CreateUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int userId);
    }

}