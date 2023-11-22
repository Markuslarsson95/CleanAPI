using Domain.Models;

namespace Infrastructure.Database
{
    public class MockDatabase
    {
        public List<Dog> Dogs
        {
            get { return allDogs; }
            set { allDogs = value; }
        }

        public List<Cat> Cats
        {
            get { return allCats; }
            set { allCats = value; }
        }

        private static List<Dog> allDogs = new()
        {
            new Dog { Id = Guid.NewGuid(), Name = "Björn"},
            new Dog { Id = Guid.NewGuid(), Name = "Patrik"},
            new Dog { Id = Guid.NewGuid(), Name = "Alfred"},
            new Dog { Id = new Guid("12345678-1234-5678-1234-567812345678"), Name = "TestDogForUnitTests"},
            new Dog { Id = new Guid("12345678-1234-5678-1234-867428755756"), Name = "TestDogForUpdateUnitTests"},
            new Dog { Id = new Guid("12345678-1234-5678-1234-573295756761"), Name = "TestDogForDeleteUnitTests"},
        };

        private static List<Cat> allCats = new()
        {
            new Cat { Id = Guid.NewGuid(), Name = "Sune", LikesToPlay = true},
            new Cat { Id = Guid.NewGuid(), Name = "Alfons", LikesToPlay = false},
            new Cat { Id = Guid.NewGuid(), Name = "Sigge", LikesToPlay = true},
            new Cat { Id = new Guid("12345678-1234-5678-1234-372653665237"), Name = "TestCatForUnitTests", LikesToPlay = true},
            new Cat { Id = new Guid("12345678-1234-5678-1234-677758277550"), Name = "TestCatForUpdateUnitTests", LikesToPlay = true},
            new Cat { Id = new Guid("12345678-1234-5678-1234-472756427786"), Name = "TestCatForDeleteUnitTests", LikesToPlay = false},
        };
    }
}
