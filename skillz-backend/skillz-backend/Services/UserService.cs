using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using skillz_backend.data;
using skillz_backend.models;
using skillz_backend.Repositories;

namespace skillz_backend.Services
{

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }
        public async Task<User> GetUserByIdAsync(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("UserId should be a positive integer.");
            }

            return await _userRepository.GetUserByIdAsync(userId);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException("Username cannot be null or empty.");
            }

            return await _userRepository.GetUserByUsernameAsync(username);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email) || !email.Contains("@"))
            {
                throw new ArgumentException("Invalid email address.");
            }

            return await _userRepository.GetUserByEmailAsync(email);
        }
        public async Task<List<Job>> GetJobsByUserIdAsync(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("UserId should be a positive integer.");
            }

            return await _userRepository.GetJobsByUserIdAsync(userId);
        }

        public async Task<List<ReviewUser>> GetReviewsByUserIdAsync(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("UserId should be a positive integer.");
            }

            return await _userRepository.GetReviewsByUserIdAsync(userId);
        }

        public async Task<List<CertificatUser>> GetUserCertificatesByUserIdAsync(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("UserId should be a positive integer.");
            }

            return await _userRepository.GetUserCertificatesByUserIdAsync(userId);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<List<UserBadge>> GetUserBadgesByUserIdAsync(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("UserId should be a positive integer.");
            }

            return await _userRepository.GetUserBadgesByUserIdAsync(userId);
        }

        public async Task CreateUserWithPlainTextPasswordAsync(User user, string plainTextPassword)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User object cannot be null.");
            }

            var existingUserById = await _userRepository.GetUserByIdAsync(user.UserId);
            if (existingUserById != null)
            {
                throw new InvalidOperationException("A user with the same UserId already exists.");
            }

            var existingUserByUsername = await _userRepository.GetUserByUsernameAsync(user.Username);
            if (existingUserByUsername != null)
            {
                throw new InvalidOperationException("A user with the same username already exists.");
            }

            var existingUserByEmail = await _userRepository.GetUserByEmailAsync(user.Email);
            if (existingUserByEmail != null)
            {
                throw new InvalidOperationException("A user with the same email address already exists.");
            }

            using var hmac = new HMACSHA512();

            user.PasswordSalt = hmac.Key;
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(plainTextPassword));

            await _userRepository.CreateUserAsync(user);
        }


        public async Task UpdateUserAsync(User user)
        {
            if (user == null || user.UserId <= 0)
            {
                throw new ArgumentException("Invalid user object or UserId.");
            }

            await _userRepository.UpdateUserAsync(user);
        }

        public async Task DeleteUserAsync(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("UserId should be a positive integer.");
            }

            await _userRepository.DeleteUserAsync(userId);
        }


    }
}
