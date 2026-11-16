namespace Lab10_NagelyArpi.Domain.Entities;

public sealed class Ticket
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
    public string Status { get; init; } = "abierto";
    public DateTime CreatedAt { get; init; }
    public DateTime? ClosedAt { get; init; }
}
