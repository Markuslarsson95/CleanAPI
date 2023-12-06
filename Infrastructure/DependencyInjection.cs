using Domain.Models;
using Domain.Repositories;
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
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddSingleton<MockDatabase>();
            services.AddDbContext<MySqlDB>(/*options =>*/
            //{
            //    //options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=animals;Uid=root;Pwd=1234;");
            //    //options.UseMySQL("server=127.0.0.1\\mssqllocaldb;uid=root;pwd=1234;database=animals");
            //}
            );
            return services;
        }
    }
}
