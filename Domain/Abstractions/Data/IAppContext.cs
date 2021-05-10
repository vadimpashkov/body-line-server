﻿using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
 using Domain.Entities;
 using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Domain.Abstractions.Data
{
    public interface IAppContext: IUnitOfWorkCreator
    {
        DbSet<User> Users { get; set; }
        DbSet<MassageType> MassagesType { get; set; }
        DbSet<Masseur> Massaures { get; set; }
        DbSet<Photos> Photos { get; set; }
        DbSet<Record> Records { get; set; }
        DbSet<Consultation> Consultations { get; set; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class, IEntity;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        EntityEntry<TEntity> Entry<TEntity>([NotNull] TEntity entity)
            where TEntity : class, IEntity;
    }
}