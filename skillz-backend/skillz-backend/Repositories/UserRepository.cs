using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using skillz_backend.data;
using skillz_backend.models;

namespace skillz_backend.Repositories
{

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

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<string> GetUserLocationByIdAsync(int userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            return user?.Location;
        }


        public async Task<List<Job>> GetJobsByUserIdAsync(int userId)
        {
            var user = await _dbContext.Users
                .Include(u => u.Jobs)
                .FirstOrDefaultAsync(u => u.UserId == userId);

            return user?.Jobs ?? new List<Job>();
        }

        public async Task<List<ReviewUser>> GetReviewsByUserIdAsync(int userId)
        {
            var user = await _dbContext.Users
                .Include(u => u.ReviewsUser)
                .FirstOrDefaultAsync(u => u.UserId == userId);

            return user?.ReviewsUser ?? new List<ReviewUser>();
        }

        public async Task<List<CertificatUser>> GetUserCertificatesByUserIdAsync(int userId)
        {
            var user = await _dbContext.Users
                .Include(u => u.UserCertificates)
                .FirstOrDefaultAsync(u => u.UserId == userId);

            return user?.UserCertificates ?? new List<CertificatUser>();
        }

        public async Task<List<UserBadge>> GetUserBadgesByUserIdAsync(int userId)
        {
            var user = await _dbContext.Users
                .Include(u => u.UserBadges)
                .FirstOrDefaultAsync(u => u.UserId == userId);

            return user?.UserBadges ?? new List<UserBadge>();
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
