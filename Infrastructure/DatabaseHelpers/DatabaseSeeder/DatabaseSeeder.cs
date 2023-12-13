using Domain.Models;
using Domain.Models.Animal;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DatabaseHelpers.DatabaseSeeder
{
    public class DatabaseSeeder
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            SeedDogs(modelBuilder);
            SeedCats(modelBuilder);
            SeedBirds(modelBuilder);
            SeedUsers(modelBuilder);
            // Add more methods for other entities as needed
        }
        private static void SeedUsers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Id = Guid.NewGuid(), UserName = "admin", Password = "admin" },
                new User { Id = Guid.NewGuid(), UserName = "string", Password = "string" }
            );
        }
        private static void SeedDogs(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dog>().HasData(
                new Dog { Id = Guid.NewGuid(), Name = "Boss", Breed = "English Bulldog", Weight = 30 },
                new Dog { Id = Guid.NewGuid(), Name = "Luffsen", Breed = "Bernese Mountain Dog", Weight = 60 },
                new Dog { Id = Guid.NewGuid(), Name = "Pim", Breed = "Cocker Spaniel", Weight = 15 }
            );
        }

        private static void SeedCats(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cat>().HasData(
                new Cat { Id = Guid.NewGuid(), Name = "Kajsa", Breed = "British Shorthair", Weight = 4, LikesToPlay = true },
                new Cat { Id = Guid.NewGuid(), Name = "Sigge", Breed = "Maine Coon", Weight = 10, LikesToPlay = true },
                new Cat { Id = Guid.NewGuid(), Name = "Lisa", Breed = "Ragdoll", Weight = 8, LikesToPlay = true }
            );
        }

        private static void SeedBirds(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bird>().HasData(
                new Bird { Id = Guid.NewGuid(), Name = "Peppe", CanFly = true, Color = "Blue" },
                new Bird { Id = Guid.NewGuid(), Name = "Charlie", CanFly = true, Color = "Yellow" },
                new Bird { Id = Guid.NewGuid(), Name = "Kiwi", CanFly = false, Color = "Green" }
            );
        }
    }
}
