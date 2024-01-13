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
        Task<string> GetUserLocationByIdAsync(int userId);
        Task<List<User>> GetAllUsersAsync(); 
        Task<List<Job>> GetJobsByUserIdAsync(int userId);
        Task<List<ReviewUser>> GetReviewsByUserIdAsync(int userId);
        Task<List<CertificatUser>> GetUserCertificatesByUserIdAsync(int userId);
        Task<List<UserBadge>> GetUserBadgesByUserIdAsync(int userId);
        Task CreateUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int userId);
    }

}