using DevWorld.LaContessa.Domain;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Persistance.SoftDelete;

public abstract class CascadingSoftDeletion<T, TDbContext> : ICascadingSoftDeletion<T, TDbContext>
    where T : ISoftDeletable
    where TDbContext : DbContext
{
    public abstract Task HandleCascadingSoftDeletionAsync(T entity, TDbContext dbContext,
        CancellationToken cancellationToken);

    public async Task HandleCascadingSoftDeletionAsync(object entity, DbContext dbContext,
        CancellationToken cancellationToken)
    {
        if (entity is T typedEntity)
        {
            await HandleCascadingSoftDeletionAsync(typedEntity, (TDbContext)dbContext, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
