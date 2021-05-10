﻿using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace WebApp.Identity
{
    public static class IdentityExtensions
    {
        public static IServiceCollection InstallIdentity(this IServiceCollection services, bool isDevelopment,
            string issuer, string audience, string secretKey, bool isQa)
        {
            services
                .AddSingleton<ReCaptchaValidator>()
                .AddAuthentication(opts =>
                {
                    opts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opts.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                    opts.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.Events = new JwtBearerEvents
                    {
                        OnForbidden = context =>
                        {
                            context.Response.StatusCode = StatusCodes.Status403Forbidden;
                            return Task.CompletedTask;
                        },
                        OnAuthenticationFailed = context =>
                        {
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            return Task.CompletedTask;
                        }
                    };

                    if (isQa)
                        options.Events.OnMessageReceived = OnMessageReceived;

                    options.RequireHttpsMetadata = !isDevelopment;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = issuer,
                        ValidAudience = audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                    };
                });

            return services;
        }

        private static Task OnMessageReceived(MessageReceivedContext context)
        {
            const string authScheme = "Bearer ";

            var authHeader = context.HttpContext.Request.Headers["Auth"].ToString();
            if (!string.IsNullOrEmpty(authHeader) &&
                authHeader.StartsWith(authScheme, StringComparison.OrdinalIgnoreCase))
            {
                context.Token = authHeader.Substring(authScheme.Length).Trim();
            }

            return Task.CompletedTask;
        }
    }
}
