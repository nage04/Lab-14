namespace Lab10_NagelyArpi.Domain.Entities;

public sealed class UserRole
{
    public Guid UserId { get; init; }
    public Guid RoleId { get; init; }
    public DateTime AssignedAt { get; init; }
}
