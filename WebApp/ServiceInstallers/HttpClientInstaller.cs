using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApp.ServiceInstallers.Abstractions;

namespace WebApp.ServiceInstallers
{
    public class HttpClientInstaller: IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
        {
            services
                .AddHttpClient();
        }
    }
}