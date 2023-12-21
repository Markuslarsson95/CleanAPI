using Application.Commands.Users.LoginUser;
using Infrastructure.Repositories.Dogs;
using Infrastructure.Repositories.Login;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Test.IntegrationTests.ControllerTests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        public Mock<IDogRepository> DogRepositoryMock { get; }
        public Mock<ILoginRepository> LoginRepositoryMock { get; }

        public CustomWebApplicationFactory()
        {
            DogRepositoryMock = new Mock<IDogRepository>();
            LoginRepositoryMock = new Mock<ILoginRepository>();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);

            builder.ConfigureTestServices(services =>
            {
                services.AddSingleton(DogRepositoryMock.Object);
                services.AddSingleton(LoginRepositoryMock.Object);
            });
        }
    }
}
