﻿using DevWorld.LaContessa.Command.Abstractions.Booking;
using DevWorld.LaContessa.Command.Abstractions.Subscription;
using DevWorld.LaContessa.Command.Abstractions.Users;
using DevWorld.LaContessa.Command.Activity;
using DevWorld.LaContessa.Command.Booking;
using DevWorld.LaContessa.Command.Subscription;
using DevWorld.LaContessa.Command.Users;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DevWorld.LaContessa.Command;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommandServices(this IServiceCollection services)
    {
        services.AddMediatR(x =>
        {
            x.RegisterServicesFromAssemblyContaining(typeof(ServiceCollectionExtensions));
        });

        services.AddTransient<IRequestHandler<CreateUser>, CreateUserHandler>();
        services.AddTransient<IRequestHandler<CreateSubscription>, CreateSubscriptionHandler>();
        services.AddTransient<IRequestHandler<UpdateSbscription>, UpdateSubscriptionHandler>();
        services.AddTransient<IRequestHandler<CreateBooking>, CreateBookingHandler>();
        services.AddTransient<IRequestHandler<UpdateBooking>, UpdateBookingHandler>();
        services.AddTransient<IRequestHandler<CreateActivity>, CreateActivityHandler>();
        services.AddTransient<IRequestHandler<UpdateActivity>, UpdateActivityHandler>();

        return services;
    }
}
