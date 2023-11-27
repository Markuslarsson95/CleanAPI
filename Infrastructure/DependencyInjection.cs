using Infrastructure.Database;
using Infrastructure.RealDatabase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<MockDatabase>();
            services.AddDbContext<MySqlDB>(options =>
            {
                options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Test");
                //options.UseSqlServer("SERVER=localhost;DATABASE=animals;UID=root;PASSWORD=1234;");
            });
            return services;
        }
    }
}
