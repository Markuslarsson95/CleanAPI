using Infrastructure.Database;
using Infrastructure.RealDatabase;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IDogRepository, DogRepository>();
            services.AddScoped<ICatRepository, CatRepository>();
            services.AddScoped<IBirdRepository, BirdRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ILoginRepository, LoginRepository>();
            services.AddScoped(typeof(IAnimalRepository<>), typeof(AnimalRepository<>));
            services.AddSingleton<MockDatabase>();
            services.AddDbContext<MySqlDB>();

            return services;
        }
    }
}
