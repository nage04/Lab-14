namespace Lab10_NagelyArpi.Domain.Entities;

public sealed class User
{
    public Guid Id { get; init; }
    public string Username { get; init; } = string.Empty;
    public string? Email { get; init; }
    public string PasswordHash { get; init; } = string.Empty;
}
