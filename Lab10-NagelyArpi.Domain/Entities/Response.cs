namespace Lab10_NagelyArpi.Domain.Entities;

public sealed class Response
{
    public Guid Id { get; init; }
    public Guid TicketId { get; init; }
    public Guid ResponderId { get; init; }
    public string Message { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }
}
