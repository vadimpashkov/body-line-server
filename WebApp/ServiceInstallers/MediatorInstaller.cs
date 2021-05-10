using Domain.Extensions;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Schedule.Implementations;
using WebApp.ServiceInstallers.Abstractions;

namespace Schedule.ServiceInstallers
{
    public class MediatorInstaller: IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
        {
            services.AddMediatR(x => x.AsScoped(),
                typeof(DomainExtensions).Assembly,
                typeof(IServiceInstaller).Assembly
            );
            
            services.AddScoped<IUseCaseDispatcher, UseCaseDispatcher>();
        }
    }
}
