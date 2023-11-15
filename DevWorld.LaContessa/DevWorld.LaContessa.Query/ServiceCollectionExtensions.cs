﻿using DevWorld.LaContessa.Query.Abstractions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DevWorld.LaContessa.Query;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddQueryServices(
        this IServiceCollection services
    )
    {
        services.AddTransient<IRequestHandler<GetUsers, GetUsers.Response>, GetUsersHandler>();
        services.AddTransient<IRequestHandler<GetSubscriptions, GetSubscriptions.Response>, GetSubscriptionsHandler>();
        services.AddTransient<IRequestHandler<GetSubscription, GetSubscription.Response>, GetSubscriptionHandler>();
        services.AddTransient<IRequestHandler<GetSubscriptionByUserId, GetSubscriptionByUserId.Response>, GetSubscriptionByUserIdHandler>();

        return services;
    }
}
