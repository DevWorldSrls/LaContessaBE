﻿namespace DevWorld.LaContessa.SetupJob;

public class Program
{
    public static async Task Main(string[] args)
    {
        await CreateHostBuilder(args)
            .Build()
            .RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host
            .CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) => { services.AddSetupJobServices(hostContext.Configuration); });
    }
}
