﻿using System;
using System.Linq;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.Data
{
    public static class DatabaseInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var sp = scope.ServiceProvider;

            var dbContext = sp.GetRequiredService<AppDbContext>();
            dbContext.Database.Migrate();

            var userManager = sp.GetRequiredService<UserManager<User>>();
            var rolesManager = sp.GetRequiredService<RoleManager<IdentityRole<int>>>();
            var logger = sp.GetService<ILogger<AppDbContext>>();
            var config = sp.GetService<IOptions<AdminConfiguration>>();

            using var txn = dbContext.Database.BeginTransaction();
            Seed(dbContext, userManager, rolesManager, logger, config.Value);

            txn.Commit();
        }

        private static void Seed(AppDbContext dbContext, UserManager<User> userManager,
            RoleManager<IdentityRole<int>> roleManager, ILogger<AppDbContext> logger, 
            AdminConfiguration config)
        {
            try
            {
                var roles = Enum.GetNames(typeof(UserRole));
                var dbRoles = dbContext.Roles.ToList();
                if (roles.Length > dbRoles.Count)
                {
                    var rolesToAdd = roles.Where(r => dbRoles.All(dbr => dbr.Name != r));
                    foreach (var role in rolesToAdd)
                    {
                        roleManager.CreateAsync(new IdentityRole<int>(role)).GetAwaiter().GetResult();
                    }
                }

                dbContext.SaveChanges();

                var user = new User(DateTime.Now, config.AdminUserName);

                userManager.CreateAsync(user).GetAwaiter().GetResult();
                userManager.AddPasswordAsync(user, config.AdminPassword).GetAwaiter().GetResult();
                userManager.AddToRoleAsync(user, UserRole.Admin.ToString()).GetAwaiter().GetResult();
                
                dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                logger.LogCritical(e.Message);

                throw;
            }
        }
    }
}