using Microsoft.EntityFrameworkCore;
using skillz_backend.models;

namespace skillz_backend.data
{
    public class SkillzDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=LAPTOP-KT7UFLKK\SQLEXPRESS;Database=SkillzDatabase;Trusted_Connection=True;Encrypt=False;");
        }
    }
}
