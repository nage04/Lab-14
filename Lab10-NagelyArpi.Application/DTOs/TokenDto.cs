namespace Lab10_NagelyArpi.Application.DTOs;

public sealed class TokenDto(string accessToken, DateTime expiresAtUtc)
{
    public string AccessToken { get; set; } = accessToken;
    public string TokenType { get; set; } = "Bearer";
    public DateTime ExpiresAtUtc { get; set; } = expiresAtUtc;
}
