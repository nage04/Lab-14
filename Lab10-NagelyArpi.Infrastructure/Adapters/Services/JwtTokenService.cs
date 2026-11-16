using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Lab10_NagelyArpi.Application.Ports.Services;
using Lab10_NagelyArpi.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Lab10_NagelyArpi.Infrastructure.Adapters.Services;

public sealed class JwtTokenService(IConfiguration configuration) : ITokenService
{
    public (string Token, DateTime ExpiresAtUtc) CreateAccessToken(User user)
    {
        var settings = configuration.GetSection("Jwt");
        var issuer = settings["Issuer"] ?? throw new InvalidOperationException("Falta configurar Jwt:Issuer.");
        var audience = settings["Audience"] ?? throw new InvalidOperationException("Falta configurar Jwt:Audience.");
        var signingKey = settings["SigningKey"] ?? throw new InvalidOperationException("Falta configurar Jwt:SigningKey.");
        if (Encoding.UTF8.GetByteCount(signingKey) < 32)
            throw new InvalidOperationException("Jwt:SigningKey debe tener al menos 32 bytes.");

        var now = DateTime.UtcNow;
        var expiresAtUtc = now.AddMinutes(settings.GetValue("ExpirationMinutes", 60));
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username)
        };
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey)),
            SecurityAlgorithms.HmacSha256);
        var jwt = new JwtSecurityToken(issuer, audience, claims, now, expiresAtUtc, credentials);
        return (new JwtSecurityTokenHandler().WriteToken(jwt), expiresAtUtc);
    }
}
