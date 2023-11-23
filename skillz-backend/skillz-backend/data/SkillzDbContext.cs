using Microsoft.EntityFrameworkCore;
using skillz_backend.models;
using System.Security.Cryptography;

namespace skillz_backend.data
{
    public class SkillzDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public SkillzDbContext(DbContextOptions<SkillzDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurarea relației dintre User și tabelul Users
            modelBuilder.Entity<User>().ToTable("Users");
        }
    }
}
