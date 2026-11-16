using Lab10_NagelyArpi.Domain.Repositories;
using Lab10_NagelyArpi.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using DomainTicket = Lab10_NagelyArpi.Domain.Entities.Ticket;

namespace Lab10_NagelyArpi.Infrastructure.Adapters.Repositories;

public sealed class TicketRepository(TicketeraBdContext context) : ITicketRepository
{
    public async Task<DomainTicket> AddAsync(DomainTicket ticket, CancellationToken cancellationToken = default)
    {
        var entity = new Lab10_NagelyArpi.Infrastructure.Context.Ticket
        {
            TicketId = ticket.Id, UserId = ticket.UserId, Title = ticket.Title,
            Description = ticket.Description, Status = ticket.Status,
            CreatedAt = ticket.CreatedAt, ClosedAt = ticket.ClosedAt
        };
        context.Tickets.Add(entity);
        return ticket;
    }

    public async Task<IReadOnlyList<DomainTicket>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await context.Tickets.AsNoTracking().OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
        return entities.Select(Map).ToList();
    }

    public async Task<DomainTicket?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await context.Tickets.AsNoTracking().FirstOrDefaultAsync(x => x.TicketId == id, cancellationToken);
        return entity is null ? null : Map(entity);
    }

    public async Task<IReadOnlyList<DomainTicket>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var entities = await context.Tickets.AsNoTracking().Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CreatedAt).ToListAsync(cancellationToken);
        return entities.Select(Map).ToList();
    }

    public async Task<bool> UpdateStatusAsync(Guid id, string status, DateTime? closedAt, CancellationToken cancellationToken = default)
    {
        var entity = await context.Tickets.FirstOrDefaultAsync(x => x.TicketId == id, cancellationToken);
        if (entity is null) return false;
        entity.Status = status;
        entity.ClosedAt = closedAt;
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await context.Tickets.FirstOrDefaultAsync(x => x.TicketId == id, cancellationToken);
        if (entity is null) return false;
        context.Tickets.Remove(entity);
        return true;
    }

    private static DomainTicket Map(Lab10_NagelyArpi.Infrastructure.Context.Ticket x) => new()
    {
        Id = x.TicketId, UserId = x.UserId, Title = x.Title, Description = x.Description,
        Status = x.Status, CreatedAt = x.CreatedAt ?? DateTime.MinValue, ClosedAt = x.ClosedAt
    };
}
