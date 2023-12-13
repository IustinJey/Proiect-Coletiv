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
        Task<List<User>> GetAllUsersAsync();
        Task<List<Job>> GetJobsByUserIdAsync(int userId);
        Task<List<ReviewUser>> GetReviewsByUserIdAsync(int userId);
        Task<List<CertificatUser>> GetUserCertificatesByUserIdAsync(int userId);
        Task<List<UserBadge>> GetUserBadgesByUserIdAsync(int userId);
        Task CreateUserWithPlainTextPasswordAsync(User user, string plainTextPassword);
        Task UpdateUserAsync(User user, string plainTextPassword);
        Task DeleteUserAsync(int userId);
    
    }

}