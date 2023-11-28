using Microsoft.Extensions.Hosting;

namespace DevWorld.LaContessa.SetupJob;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args)
            .Build()
            .RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host
            .CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddSetupJobServices(hostContext.Configuration);
            });
}
