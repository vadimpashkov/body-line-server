using System;
using System.Linq;
using Domain.Abstractions.Outputs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Schedule.Presentation;
using WebApp.ServiceInstallers.Abstractions;

namespace Schedule.ServiceInstallers
{
    public class PresentersInstaller: IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
        {
            typeof(IOutput).Assembly.ExportedTypes
                .Where(x => typeof(IOutput).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .ToList()
                .ForEach(output =>
                {
                    var presenterType = typeof(IPresenter<>).MakeGenericType(output);
                    
                    var presenter = typeof(IServiceInstaller).Assembly.ExportedTypes
                        .FirstOrDefault(x => presenterType.IsAssignableFrom(x)
                                             && !x.IsInterface && !x.IsAbstract);

                    if (presenter is null)
                    {
                        throw new Exception($"There is no presenter for {output.Name}");
                    }

                    services.AddScoped(presenterType, presenter);
                });
        }
    }
}