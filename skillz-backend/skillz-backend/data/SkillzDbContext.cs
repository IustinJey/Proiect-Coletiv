using Microsoft.EntityFrameworkCore;
using skillz_backend.models;
using System.Security.Cryptography;

namespace skillz_backend.data
{
    public class SkillzDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-6IPRV98\SQLEXPRESS;Database=SkillzDatabase;Trusted_Connection=True;Encrypt=False;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurarea relației dintre User și tabelul Users
            modelBuilder.Entity<User>().ToTable("Users");
        }
    }
}