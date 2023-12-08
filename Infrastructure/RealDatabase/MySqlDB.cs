﻿using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.RealDatabase
{
    public class MySqlDB : DbContext
    {
        public MySqlDB() { }

        public MySqlDB(DbContextOptions<MySqlDB> options) : base(options) { }

        public DbSet<Dog> Dogs { get; set; }
        public DbSet<Bird> Birds { get; set; }
        public DbSet<Cat> Cats { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=animals;user=root;password=1234");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dog>(entity => { entity.Property(e => e.Name).IsRequired(); });
            modelBuilder.Entity<User>(entity => { entity.Property(e => e.UserName).IsRequired(); });
            modelBuilder.Entity<User>(entity => { entity.Property(e => e.Password).IsRequired(); });

            modelBuilder.Entity<Dog>().HasData(
                new Dog { Id = Guid.NewGuid(), Name = "Boss" },
                new Dog { Id = Guid.NewGuid(), Name = "Luffsen" },
                new Dog { Id = Guid.NewGuid(), Name = "Pim" });

            modelBuilder.Entity<User>().HasData(
                new User { Id = Guid.NewGuid(), UserName = "string", Password = "string" },
                new User { Id = Guid.NewGuid(), UserName = "admin", Password = "admin" });
        }
    }
}
