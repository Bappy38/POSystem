using Microsoft.Extensions.DependencyInjection;
using POSystem.Domain.Repositories;
using POSystem.Infrastructure.Data;
using POSystem.Infrastructure.Repositories;

namespace POSystem.Infrastructure.Extenstions;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<DbContext>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        return services;
    }
}
