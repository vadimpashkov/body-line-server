﻿using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebApp.ServiceInstallers.Abstractions
{
    public static class InstallerExtension
    {
        public static void InstallFromAssembly(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
        {
            typeof(IServiceInstaller).Assembly.ExportedTypes
                .Where(x => typeof(IServiceInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<IServiceInstaller>()
                .ToList()
                .ForEach(service =>
                {
                    service.Install(services, configuration, env);
                });
        }
    }
}