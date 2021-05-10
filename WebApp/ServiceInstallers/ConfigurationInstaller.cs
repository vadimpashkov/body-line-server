using Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Schedule.Configuration;
using WebApp.ServiceInstallers.Abstractions;

namespace Schedule.ServiceInstallers
{
    public class ConfigurationInstaller: IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
        {
            services
                .Configure<AdminConfiguration>(configuration.GetSection("Admin"))
                .Configure<AuthConfiguration>(configuration.GetSection("Auth"))
                .Configure<CaptchaConfiguration>(configuration.GetSection("ReCaptcha"));
        }
    }
}