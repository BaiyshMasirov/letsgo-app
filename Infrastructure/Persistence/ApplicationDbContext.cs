using Application.Common.Interfaces;
using Domain.Common;
using Domain.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Reflection;
using System.Security.Claims;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>, IApplicationDbContext
    {
        private IDbContextTransaction _currentTransaction;
        private readonly IDateTime _dateTime;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
                             IDateTime dateTime) : base(options)
        {
            _dateTime = dateTime;
        }

        public override Task<int> SaveChangesAsync(CancellationToken token = new CancellationToken())
        {
            var userId = Guid.NewGuid();
            var httpContextAccessor = this.GetService<IHttpContextAccessor>();
            string userIdVal = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!string.IsNullOrEmpty(userIdVal))
                userId = Guid.Parse(userIdVal);

            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = userId;
                        entry.Entity.Created = _dateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.ModifiedBy = userId;
                        entry.Entity.Modified = _dateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(token);
        }

        public void SetEntityState(object entity, EntityState entityState)
        {
            base.Entry(entity).State = entityState;
        }

        public async Task BeginTransactionAsync()
        {
            if (_currentTransaction != null)
                return;

            _currentTransaction = await base.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted).ConfigureAwait(false);
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await SaveChangesAsync().ConfigureAwait(false);

                await _currentTransaction?.CommitAsync();
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public async Task RollbackTransactionAsync()
        {
            try
            {
                await _currentTransaction?.RollbackAsync();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        } 
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}