﻿using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using DevWorld.LaContessa.API.Middleware;
using DevWorld.LaContessa.Command;
using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Stripe;
using DevWorld.LaContessa.Persistance.Migrations;
using DevWorld.LaContessa.Query;
using MediatR;

namespace DevWorld.LaContessa.API;

[ExcludeFromCodeCoverage]
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
        services.AddApiServices(_configuration);
        services.AddQueryServices();
        services.AddCommandServices();
        services.AddStripeServices(_configuration);
        services.AddPersistance(
            _configuration.GetSection("Persistance"),
            Assembly.GetAssembly(typeof(MigrationsAssemblyReferenceClass))!
        );
    }

#pragma warning disable IDE0060 // Remove unused parameter
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
#pragma warning restore IDE0060 // Remove unused parameter
    {
        app.UseRouting();
        app.UseCors("LaContessaPolicy");

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseMiddleware<ErrorHandlerMiddleware>();
        app.UseForwardedHeaders();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();

            //TODO: Need to add health check??
        });

        app.UseSwagger();
        app.UseSwaggerUI();
    }
}
