﻿using System.Threading.Tasks;
using Domain.Abstractions.Data;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Data
{
    public class AppDbContext: IdentityDbContext<User, IdentityRole<int>, int, IdentityUserClaim<int>, 
            UserRoleEntity, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>, 
        IAppContext
    {
        private IUnitOfWork _currentUnitOfWork;
        public AppDbContext(DbContextOptions options)
            : base(options) {  }
        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<MassageType> MassagesType { get; set;}
        public DbSet<Masseur> Massaures { get; set;}
        public DbSet<Photos> Photos { get; set;}
        public DbSet<Record> Records { get; set;}
        public DbSet<Consultation> Consultations { get; set; }

        public new DbSet<TEntity> Set<TEntity>() where TEntity : class, IEntity
            => base.Set<TEntity>();

        public new EntityEntry<TEntity> Entry<TEntity>(TEntity entity) 
            where TEntity : class, IEntity
        {
            return base.Entry(entity);
        }

        public IUnitOfWork CreateUnitOfWork()
        {
            if (_currentUnitOfWork != null)
                return new DumbTransaction();

            _currentUnitOfWork = new Transaction(Database.BeginTransaction(), this);
            return _currentUnitOfWork;
        }
        private class Transaction : IUnitOfWork
        {
            private readonly IDbContextTransaction _transaction;
            private readonly AppDbContext _dbContext;

            public Transaction(IDbContextTransaction transaction, AppDbContext dbContext)
            {
                _transaction = transaction;
                _dbContext = dbContext;
            }

            public void Dispose()
            {
                _transaction.Dispose();
                _dbContext._currentUnitOfWork = null;
            }
            public async Task Apply()
            {
                await _dbContext.SaveChangesAsync();
                await _transaction.CommitAsync();
            }

            public Task Cancel() => _transaction.RollbackAsync();
        }
        private class DumbTransaction : IUnitOfWork
        {
            public void Dispose()
            {
            }

            public Task Apply() => Task.CompletedTask;
            public Task Cancel() => Task.CompletedTask;
        }
    }
}
