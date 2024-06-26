﻿using Microsoft.Extensions.DependencyInjection;
using POSystem.Application.Interfaces;
using POSystem.Application.Services;

namespace POSystem.Application.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IOrderService, OrderService>();

        return services;
    }
}
