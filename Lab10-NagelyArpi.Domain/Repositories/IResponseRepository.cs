using Lab10_NagelyArpi.Domain.Entities;

namespace Lab10_NagelyArpi.Domain.Repositories;

public interface IResponseRepository
{
    Task<Response> AddAsync(Response response, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Response>> GetByTicketIdAsync(Guid ticketId, CancellationToken cancellationToken = default);
}
