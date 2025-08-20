using Microsoft.EntityFrameworkCore;
using MinimalApi.Domain.Entities;

namespace MinimalApi.Infra.Db
{
    public class DbContexto : DbContext
    {
        private readonly IConfiguration _configuration;
        public DbContexto(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrator>().HasData(
                new Administrator
                {
                    Id = 1,
                    Name = "admin",
                    Email = "admin@admin.com",
                    Password = "admin",
                    Profile = Domain.Enums.Profile.Administrator
                }
            );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;

            string? connString = _configuration.GetConnectionString("mysql");
            if (string.IsNullOrEmpty(connString))
            {
                throw new Exception("Connection string is null");
            }

            optionsBuilder.UseMySql(connString, ServerVersion.AutoDetect(connString));
        }
    }
}
