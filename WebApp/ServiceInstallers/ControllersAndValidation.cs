using Domain.Abstractions.Mediator;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApp.ServiceInstallers.Abstractions;

namespace Schedule.ServiceInstallers
{
    public class ControllersAndValidation: IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
        {
            services
                .AddControllers()
                .AddNewtonsoftJson()
                .AddFluentValidation(fv =>
                    fv.RegisterValidatorsFromAssembly(typeof(IUseCaseInput).Assembly));
        }
    }
}