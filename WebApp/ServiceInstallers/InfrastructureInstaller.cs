using Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApp.ServiceInstallers.Abstractions;

namespace Schedule.ServiceInstallers
{
    public class InfrastructureInstaller: IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
        {
            var defaultConnection = configuration.GetConnectionString("DefaultConnection");

            services.InstallInfrastructure(defaultConnection);
        }
    }
}