﻿using Domain.Abstractions.Data;
using Domain.Services;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class InfrastructureExtension
    {
        public static void InstallInfrastructure(this IServiceCollection services, string connectionString)
        {
            services
                .AddDbContextPool<AppDbContext>(options =>
                {
                    options.UseNpgsql(connectionString);
                    options.EnableSensitiveDataLogging();
                })
                .AddScoped<IAppContext, AppDbContext>()
                .AddScoped<IFileStorage, FileStorage>();
        }
    }
}
