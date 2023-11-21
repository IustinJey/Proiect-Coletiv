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

    public class UserRepository : IUserRepository
    {
        private readonly SkillzDbContext _dbContext;

        public UserRepository(SkillzDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _dbContext.Users.FindAsync(userId);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == email);
        }


        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<IEnumerable<User>> GetUsersByAgeAsync(int age)
        {
            return await _dbContext.Users.Where(u => u.Age == age).ToListAsync();
        }

        public async Task<IEnumerable<User>> GetUsersByLocationAsync(string location)
        {
            return await _dbContext.Users.Where(u => u.Location == location).ToListAsync();
        }

        public async Task<IEnumerable<User>> GetUsersByPhoneNumberAsync(string phoneNumber)
        {
            return await _dbContext.Users.Where(u => u.PhoneNumber == phoneNumber).ToListAsync();
        }

        public async Task<IEnumerable<User>> GetVerifiedUsersAsync()
        {
            return await _dbContext.Users.Where(u => u.Verified).ToListAsync();
        }

        public async Task CreateUserAsync(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _dbContext.Entry(user).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int userId)
        {
            var userToDelete = await _dbContext.Users.FindAsync(userId);
            if (userToDelete != null)
            {
                _dbContext.Users.Remove(userToDelete);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
