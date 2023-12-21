using Domain.Models;
using Domain.Models.Animals;
using Infrastructure.DatabaseHelpers.DatabaseSeeder;
using Infrastructure.Repositories.Password;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.RealDatabase
{
    public class SqlDbContext : DbContext
    {
        public SqlDbContext() { }

        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Dog> Dogs { get; set; }
        public DbSet<Bird> Birds { get; set; }
        public DbSet<Cat> Cats { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Mysql
            //optionsBuilder.UseMySQL("server=localhost;database=animals;user=root;password=1234");
            optionsBuilder.UseSqlServer("Server=(local)\\sqlexpress;Database=animals;Trusted_Connection=true;TrustServerCertificate=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var passwordEncryptor = new PasswordEncryptor();
            var seeder = new DatabaseSeeder(passwordEncryptor);

            seeder.SeedData(modelBuilder);
        }
    }
}
