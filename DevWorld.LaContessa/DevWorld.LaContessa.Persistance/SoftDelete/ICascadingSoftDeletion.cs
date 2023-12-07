using DevWorld.LaContessa.Domain;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Persistance.SoftDelete;

public interface ICascadingSoftDeletion<T, TDbContext> : ICascadingSoftDeletion
    where T : ISoftDeletable
    where TDbContext : DbContext
{
    public Task HandleCascadingSoftDeletionAsync(T entity, TDbContext dbContext, CancellationToken cancellationToken);
}

public interface ICascadingSoftDeletion
{
    public Task HandleCascadingSoftDeletionAsync(object entity, DbContext dbContext,
        CancellationToken cancellationToken);
}
