using Domain.Abstractions.Mediator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApp.ServiceInstallers.Abstractions;

namespace Schedule.ServiceInstallers
{
    public class MapperInstaller: IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
        {
            services.AddAutoMapper(
                typeof(IUseCaseInput).Assembly
            );
        }
    }
}