using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using skillz_backend.data;
using skillz_backend.models;

namespace skillz_backend.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(int userId);
        Task<User> GetUserByUsernameAsync(string username);
        Task<User> GetUserByEmailAsync(string email);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<IEnumerable<User>> GetUsersByAgeAsync(int age);
        Task<IEnumerable<User>> GetUsersByLocationAsync(string location);
        Task<IEnumerable<User>> GetUsersByPhoneNumberAsync(string phoneNumber);
        Task<IEnumerable<User>> GetVerifiedUsersAsync();
        Task CreateUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int userId);
    }

}