﻿using Microsoft.Extensions.DependencyInjection;

namespace DevWorld.LaContessa.Domain;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        //Add here Domain Services as Singleton

        return services;
    }
}
