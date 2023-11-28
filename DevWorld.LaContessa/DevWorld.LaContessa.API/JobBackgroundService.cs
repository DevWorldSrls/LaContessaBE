namespace DevWorld.LaContessa.API
{
    public class JobBackgroundService : BackgroundService
    {
        private readonly ILogger<JobBackgroundService> _logger;
        private readonly IServiceProvider _serviceProvider;
        // TODO decomment after moving into setupjob project
        //private readonly IHostApplicationLifetime _hostApplicationLifetime;

        public JobBackgroundService(
            ILogger<JobBackgroundService> logger, 
            IServiceProvider serviceProvider
            // TODO decomment after moving into setupjob project
            // IHostApplicationLifetime hostApplicationLifetime
        )
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            // TODO decomment after moving into setupjob project
            //_hostApplicationLifetime = hostApplicationLifetime;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                using var serviceScope = _serviceProvider.CreateScope();

                var jobs = serviceScope.ServiceProvider.GetServices<IJob>();

                foreach (var job in jobs)
                {
                    var jobName = job.GetType().Name;

                    _logger.LogInformation("Starting job: {JobName}", jobName);

                    try
                    {
                        await job.ExecuteAsync(stoppingToken);
                    }
                    catch (OperationCanceledException ex)
                    {
                        _logger.LogWarning(ex, "Jobs service was cancelled during the job: {JobName}", jobName);
                        throw;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Jobs service failed for job: {JobName}", jobName);
                        throw;
                    }

                    _logger.LogInformation("Job completed: {JobName}", jobName);
                }
            }
            catch (OperationCanceledException)
            {
                // TODO decomment after moving into setupjob project
                //_logger.LogWarning("Job execution was cancelled, stopping application");
                //Environment.ExitCode = 1;
            }
            catch (Exception ex)
            {
                // TODO decomment after moving into setupjob project
                //_logger.LogCritical(ex, "Job execution failed, stopping application");
                //Environment.ExitCode = 2;
            }
            finally
            {
                // TODO decomment after moving into setupjob project
                //_hostApplicationLifetime.StopApplication();
            }
        }
    }
}
