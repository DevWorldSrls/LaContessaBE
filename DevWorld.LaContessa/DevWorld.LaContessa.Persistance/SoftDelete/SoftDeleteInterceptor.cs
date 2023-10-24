using DevWorld.LaContessa.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DevWorld.LaContessa.Persistance.SoftDelete;

public class SoftDeleteInterceptor : SaveChangesInterceptor
{
    private readonly IEnumerable<ICascadingSoftDeletion> _cascadingSoftDeletions;

    public SoftDeleteInterceptor(IEnumerable<ICascadingSoftDeletion> cascadingSoftDeletions)
    {
        _cascadingSoftDeletions = cascadingSoftDeletions;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result) =>
        SavingChangesAsync(eventData, result, default).ConfigureAwait(false).GetAwaiter().GetResult();

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken)
    {
        if (eventData.Context is null)
            return result;

        var entries = eventData.Context.ChangeTracker.Entries().ToList();

        foreach (var entry in entries)
        {
            if (entry is not
                {
                    State: EntityState.Deleted,
                    Entity: ISoftDeletable delete,
                }
            )
                continue;

            entry.State = EntityState.Modified;
            delete.IsDeleted = true;
            delete.DeletedAt = DateTimeOffset.UtcNow;

            foreach (var cascadingSoftDeletion in _cascadingSoftDeletions)
                await cascadingSoftDeletion.HandleCascadingSoftDeletionAsync(entry.Entity, eventData.Context, cancellationToken); 
        }

        return result;
    }
}
