using Infrastructure.Database;
using Infrastructure.RealDatabase;
using Infrastructure.Repositories.Animals;
using Infrastructure.Repositories.Birds;
using Infrastructure.Repositories.Cats;
using Infrastructure.Repositories.Dogs;
using Infrastructure.Repositories.Login;
using Infrastructure.Repositories.Password;
using Infrastructure.Repositories.Users;
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
            services.AddScoped<IPasswordEncryptor, PasswordEncryptor>();
            services.AddScoped(typeof(IAnimalRepository<>), typeof(AnimalRepository<>));
            services.AddSingleton<MockDatabase>();
            services.AddDbContext<SqlDbContext>();

            return services;
        }
    }
}
