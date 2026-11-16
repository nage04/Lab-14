using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Lab10_NagelyArpi.Configuration;

public static class JwtAuthenticationExtensions
{
    public static IServiceCollection AddJwtAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var jwt = configuration.GetSection("Jwt");
        var issuer = jwt["Issuer"] ?? throw new InvalidOperationException("Falta configurar Jwt:Issuer.");
        var audience = jwt["Audience"] ?? throw new InvalidOperationException("Falta configurar Jwt:Audience.");
        var signingKey = jwt["SigningKey"] ?? throw new InvalidOperationException(
            "Falta Jwt:SigningKey. Configúrala en secretos de usuario o en la variable de entorno Jwt__SigningKey.");
        if (Encoding.UTF8.GetByteCount(signingKey) < 32)
            throw new InvalidOperationException("Jwt:SigningKey debe tener al menos 32 bytes.");

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey)),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromSeconds(30)
                };
            });
        services.AddAuthorization();
        return services;
    }
}
