﻿using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Abstractions;
using Domain.Abstractions.Data;
using Domain.Abstractions.Mediator;
using Domain.Abstractions.Queries;
using Domain.UseCases.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Extensions
{
    public static class DomainExtensions
    {
        private static readonly List<Type> Types = typeof(IUseCaseInput).Assembly.ExportedTypes.ToList();
        public static void InstallDomain(this IServiceCollection services)
        {
            services
                .AddTransient<IDateTimeProvider, DateTimeProvider>()
                .AddQueryHandlers();
        } 

        private static IServiceCollection AddQueryHandlers(this IServiceCollection services)
        {
            var filters = Types
                .Where(x => typeof(IFilter).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .ToList();
            
            var entities = Types
                .Where(x => typeof(IEntity).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .ToList();
            
            var outputs = Types
                .Where(x => typeof(IQueryOutput).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .ToList();

            var combinations = (
                from filter in filters 
                from entity in entities 
                from output in outputs 
                select typeof(QueryHandler<,,>).MakeGenericType(filter, entity, output)
            ).ToList();
            
            combinations.ForEach(combination =>
            {
                var founded = Types.FirstOrDefault(
                    x => combination.IsAssignableFrom(x) && !x.IsAbstract && !x.IsInterface);
            
                if (!(founded is null))
                {
                    services.AddScoped(combination, founded);
                }
            });

            return services;
        }
    }
}