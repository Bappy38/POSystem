using POSystem.API.ExceptionHandlers;

namespace POSystem.API.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        services.AddControllers();

        return services;
    }
}
