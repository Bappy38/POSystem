using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using POSystem.Domain.Constants;
using POSystem.Domain.Repositories;
using POSystem.Infrastructure.Data;
using POSystem.Infrastructure.Repositories;

namespace POSystem.Infrastructure.Extenstions;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<DbContext>();

        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ISupplierRepository, SupplierRepository>();

        services
            .AddHealthChecks()
            .AddSqlServer(
                configuration.GetConnectionString(DbConstants.DefaultConnection),
                name: "SQLServer",
                healthQuery: "SELECT 1",
                failureStatus: HealthStatus.Unhealthy,
                tags: new[] { "Database", "SQL Server" });

        return services;
    }
}
