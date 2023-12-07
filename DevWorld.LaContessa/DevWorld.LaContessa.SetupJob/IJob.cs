namespace DevWorld.LaContessa.SetupJob;

public interface IJob
{
    Task ExecuteAsync(CancellationToken cancellationToken);
}