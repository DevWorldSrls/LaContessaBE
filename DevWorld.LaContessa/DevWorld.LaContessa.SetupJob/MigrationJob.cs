using DevWorld.LaContessa.Persistance;

namespace DevWorld.LaContessa.SetupJob;

public class MigrationJob : IJob
{
    private readonly LaContessaDbContext _laContessaDbContext;

    public MigrationJob(LaContessaDbContext laContessaDbContext)
    {
        _laContessaDbContext = laContessaDbContext;
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        await _laContessaDbContext.ApplyMigrationAsync(cancellationToken);
    }
}