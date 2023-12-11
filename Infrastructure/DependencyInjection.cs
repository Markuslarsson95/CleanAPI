﻿using Domain.Repositories;
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
            services.AddScoped<ILoginRepository, LoginRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddSingleton<MockDatabase>();
            services.AddDbContext<MySqlDB>();

            return services;
        }
    }
}
