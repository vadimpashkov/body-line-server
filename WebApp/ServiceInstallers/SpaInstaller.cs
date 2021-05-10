using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApp.ServiceInstallers.Abstractions;

namespace Schedule.ServiceInstallers
{
    public class SpaInstaller: IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
        {
            services.AddSpaStaticFiles(x =>
            {
                x.RootPath = "Frontend";
            });
        }
    }
}
