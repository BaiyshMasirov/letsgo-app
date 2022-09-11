using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationEFContext
    {
        DbSet<Event> Events { get; set; }
        DbSet<Location> Locations { get; set; }
        DbSet<Ticket> Tickets { get; set; }
        DbSet<RToken> RTokens { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        void SetEntityState(object entity, EntityState entityState);

        Task BeginTransactionAsync();

        Task CommitTransactionAsync();

        Task RollbackTransactionAsync();
    }
}