using Domain.Models;
using Domain.Models.Animals;
using Infrastructure.DatabaseHelpers.DatabaseSeeder;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.RealDatabase
{
    public class MySqlDB : DbContext
    {
        public MySqlDB() { }

        public MySqlDB(DbContextOptions<MySqlDB> options) : base(options) { }

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

            // Call the SeedData method from the external class
            DatabaseSeeder.SeedData(modelBuilder);
        }
    }
}
