using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApp.Identity;
using WebApp.ServiceInstallers.Abstractions;

namespace Schedule.ServiceInstallers
{
    public class IdentityInstaller: IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
        {
            var authIssuer = configuration["Auth:Issuer"];
            var authAudience = configuration["Auth:Audience"];
            var authSecretKey = configuration["Auth:SecretKey"];
            
            services
                .InstallIdentity(env.IsDevelopment(), authIssuer, authAudience, authSecretKey, configuration.GetValue<bool>("IsQa"))
                
                .AddIdentity<User, IdentityRole<int>>(config =>
                {
                    config.Password.RequiredLength = 4;
                    config.Password.RequireDigit = false;
                    config.Password.RequireLowercase = false;
                    config.Password.RequireNonAlphanumeric = false;
                    config.Password.RequireUppercase = false;
                })
                .AddUserManager<UserManager<User>>()
                .AddSignInManager<SignInManager<User>>()
                .AddEntityFrameworkStores<AppDbContext>();
        }
    }
}
