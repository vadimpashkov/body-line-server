﻿using System.Collections.Generic;
using System.Linq;
using FluentValidation;

namespace Domain.Extensions
{
    public static class ValidationExtensions
    {

        public static IRuleBuilderOptions<TEntity, string> ValidateUsername<TEntity>(this IRuleBuilder<TEntity, string> builder) 
            => builder
                .NotEmpty()
                .WithMessage("Имя пользователя не может быть пустым")
                
                .Must(x => x.Length > 4)
                .WithMessage("Имя пользователя должно быть больше 4 символов");

        public static IRuleBuilderOptions<TEntity, string> ValidatePassword<TEntity>(this IRuleBuilder<TEntity, string> builder) 
            => builder
                .NotEmpty()
                .WithMessage("Пароль не может быть пустым")
                
                .Must(x => x.Length > 4)
                .WithMessage("Пароль должен быть больше 4 символов");

        public static IRuleBuilderOptions<TEntity, IEnumerable<int>> EntityReference<TEntity>(this IRuleBuilder<TEntity, IEnumerable<int>> builder) 
            => builder.Must(x => x.Any(z => z > 0));

        public static IRuleBuilderOptions<TEntity, int> EntityReference<TEntity>(this IRuleBuilder<TEntity, int> builder) 
            => builder.GreaterThan(0);

        public static IRuleBuilderOptions<TEntity, int?> EntityReference<TEntity>(this IRuleBuilder<TEntity, int?> builder) 
            => builder.Must(x => !x.HasValue || x > 0);

    }
}
