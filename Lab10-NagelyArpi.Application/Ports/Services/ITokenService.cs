using Lab10_NagelyArpi.Domain.Entities;

namespace Lab10_NagelyArpi.Application.Ports.Services;

public interface ITokenService
{
    (string Token, DateTime ExpiresAtUtc) CreateAccessToken(User user);
}
