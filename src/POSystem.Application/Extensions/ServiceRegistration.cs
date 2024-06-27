using Microsoft.Extensions.DependencyInjection;
using POSystem.Application.Interfaces;
using POSystem.Application.Services;

namespace POSystem.Application.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var applicationAssembly = typeof(ServiceRegistration).Assembly;

        services.AddAutoMapper(applicationAssembly);

        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<ISupplierService, SupplierService>();

        return services;
    }
}
