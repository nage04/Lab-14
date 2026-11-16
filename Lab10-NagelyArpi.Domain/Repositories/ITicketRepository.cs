using Lab10_NagelyArpi.Domain.Entities;

namespace Lab10_NagelyArpi.Domain.Repositories;

public interface ITicketRepository
{
    Task<Ticket> AddAsync(Ticket ticket, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Ticket>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Ticket?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Ticket>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<bool> UpdateStatusAsync(Guid id, string status, DateTime? closedAt, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
