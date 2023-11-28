namespace DevWorld.LaContessa.API
{
    public interface IJob
    {
        Task ExecuteAsync(CancellationToken cancellationToken);
    }
}
