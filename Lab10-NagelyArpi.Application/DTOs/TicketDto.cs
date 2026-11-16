namespace Lab10_NagelyArpi.Application.DTOs;

public sealed class TicketDto
{
    public TicketDto(Guid id, Guid userId, string title, string? description, string status, DateTime createdAt, DateTime? closedAt)
    {
        Id = id;
        UserId = userId;
        Title = title;
        Description = description;
        Status = status;
        CreatedAt = createdAt;
        ClosedAt = closedAt;
    }

    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ClosedAt { get; set; }
}
