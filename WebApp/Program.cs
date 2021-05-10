using System.Linq;
using Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Schedule
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var app = CreateHostBuilder(args).Build();

            using (var services = app.Services.CreateScope())
            {
                var context = services.ServiceProvider.GetService<AppDbContext>();
                
                context.Database.Migrate();

                if (!context.Users.Any())
                {
                    DatabaseInitializer.Initialize(app.Services);
                }
                
                context.SaveChanges();
            }
            
            app.Run();
        }


        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}