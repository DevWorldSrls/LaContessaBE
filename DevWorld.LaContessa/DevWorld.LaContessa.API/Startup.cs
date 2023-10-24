﻿using DevWorld.LaContessa.API.Middleware;
using DevWorld.LaContessa.Query;
using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Command;
using MediatR;
using System.Reflection;
using DevWorld.LaContessa.Persistance.Migrations;

namespace DevWorld.LaContessa.API;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining<IMediator>());
        services.AddApiServices();
        services.AddQueryServices();
        services.AddCommandServices();
        services.AddPersistance(
            _configuration.GetSection("Persistance"),
            Assembly.GetAssembly(typeof(MigrationsAssemblyReferenceClass))!
            );
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();
        app.UseMiddleware<ErrorHandlerMiddleware>();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();

            //TODO: Need to add health check??
        });

        app.UseSwagger();
        app.UseSwaggerUI();
    }
}
