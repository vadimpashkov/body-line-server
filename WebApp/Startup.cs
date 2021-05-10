using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApp.ServiceInstallers.Abstractions;

namespace Schedule
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IHostEnvironment _env;

        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.InstallFromAssembly(_configuration, _env);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger(option =>
            {
                option.RouteTemplate = "swagger/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint("v1/swagger.json", "API");
            });

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            app.Map("/admin", app1 =>
            {
                app1.UseSpa(config =>
                {
                    config.Options.DefaultPage = "/admin/index.html";
                    config.Options.SourcePath = "Frontend";
                });
            });

            app.MapWhen(x =>
                    !x.Request.Path.StartsWithSegments("/swagger")
                    && !x.Request.Path.StartsWithSegments("/admin"),
                app1 =>
                {
                    app1.UseSpa(config =>
                    {
                        config.Options.DefaultPage = "/client/index.html";
                        config.Options.SourcePath = "Frontend";
                    });
                });
        }
    }
}
