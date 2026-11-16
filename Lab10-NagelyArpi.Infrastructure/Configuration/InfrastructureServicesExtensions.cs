using Lab10_NagelyArpi.Infrastructure.Context;
using Lab10_NagelyArpi.Domain.Repositories;
using Lab10_NagelyArpi.Infrastructure.Adapters.Repositories;
using Lab10_NagelyArpi.Infrastructure.Adapters.Services;
using Lab10_NagelyArpi.Application.Ports.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lab10_NagelyArpi.Infrastructure.Configuration;

public static class InfrastructureServicesExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "No se encontró la cadena de conexión 'DefaultConnection'. Configúrala en appsettings.json.");
        }

        services.AddDbContext<TicketeraBdContext>(options => options.UseNpgsql(connectionString));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<ITicketRepository, TicketRepository>();
        services.AddScoped<IResponseRepository, ResponseRepository>();
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddSingleton<IDateTimeProvider, SystemDateTimeProvider>();
        services.AddScoped<IPasswordService, AspNetPasswordService>();
        services.AddScoped<ITokenService, JwtTokenService>();
        return services;
    }
}
