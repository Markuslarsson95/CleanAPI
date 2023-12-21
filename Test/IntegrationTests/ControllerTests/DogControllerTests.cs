using System.Net;
using Domain.Models.Animals;
using Moq;
using Newtonsoft.Json;

namespace Test.IntegrationTests.ControllerTests
{
    public class DogControllerTests : IDisposable
    {
        private CustomWebApplicationFactory _factory;
        private HttpClient _client;

        public DogControllerTests()
        {
            _factory = new CustomWebApplicationFactory();
            _client = _factory.CreateClient();
        }

        [Test]
        public async Task Get_Always_ReturnsAllDogs()
        {
            // Arrrange
            var dogList = new List<Dog>
            {
                new Dog{ Id = Guid.NewGuid(), Name = "IntegrationTest", Breed = "Bulldog", Weight = 25 },
                new Dog{ Id = Guid.NewGuid(), Name = "IntegrationTest2", Breed = "Pitbull", Weight = 30 }
            };

            _factory.DogRepositoryMock.Setup(x => x.GetAll(null, null)).Returns(Task.FromResult(dogList));

            // Act
            var response = await _client.GetAsync("/api/Dog/getAllDogs");
            var responseData = JsonConvert.DeserializeObject<IList<Dog>>(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            foreach (var dog in responseData!)
            {
                Assert.That(dogList, Contains.Item(dog));
            }
        }

        [Test]
        public async Task GetById_ReturnsDog()
        {
            // Arrrange
            var dog = new Dog { Id = Guid.NewGuid(), Name = "Test", Breed = "Bulldog", Weight = 25 };

            _factory.DogRepositoryMock.Setup(x => x.GetById(dog.Id)).ReturnsAsync(dog);

            // Act
            var response = await _client.GetAsync($"api/Dog/getDogById/{dog.Id}");
            var responseData = JsonConvert.DeserializeObject<Dog>(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(dog, Is.EqualTo(responseData));
        }

        public void Dispose()
        {
            _client.Dispose();
            _factory.Dispose();
        }
    }
}
