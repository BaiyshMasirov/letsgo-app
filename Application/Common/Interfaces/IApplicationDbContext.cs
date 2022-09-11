using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        void SetEntityState(object entity, EntityState entityState);

        Task BeginTransactionAsync();

        Task CommitTransactionAsync();

        Task RollbackTransactionAsync();
    }
}