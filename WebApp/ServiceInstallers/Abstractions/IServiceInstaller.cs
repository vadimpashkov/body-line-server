﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebApp.ServiceInstallers.Abstractions
{
    public interface IServiceInstaller
    {
        void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment env);
    }
}