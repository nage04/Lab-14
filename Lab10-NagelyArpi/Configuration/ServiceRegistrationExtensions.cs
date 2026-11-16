using Lab10_NagelyArpi.Application.Configuration;
using Lab10_NagelyArpi.Infrastructure.Configuration;

namespace Lab10_NagelyArpi.Configuration;

public static class ServiceRegistrationExtensions
{
    public static IServiceCollection AddProjectServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddApplication();
        services.AddInfrastructure(configuration);
        services.AddJwtAuthentication(configuration);
        return services;
    }
}
