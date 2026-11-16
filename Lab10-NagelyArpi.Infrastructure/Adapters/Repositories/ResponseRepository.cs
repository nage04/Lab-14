using Lab10_NagelyArpi.Domain.Repositories;
using Lab10_NagelyArpi.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using DomainResponse = Lab10_NagelyArpi.Domain.Entities.Response;

namespace Lab10_NagelyArpi.Infrastructure.Adapters.Repositories;

public sealed class ResponseRepository(TicketeraBdContext context) : IResponseRepository
{
    public async Task<DomainResponse> AddAsync(DomainResponse response, CancellationToken cancellationToken = default)
    {
        var entity = new Lab10_NagelyArpi.Infrastructure.Context.Response
        {
            ResponseId = response.Id, TicketId = response.TicketId, ResponderId = response.ResponderId,
            Message = response.Message, CreatedAt = response.CreatedAt
        };
        context.Responses.Add(entity);
        return response;
    }

    public async Task<IReadOnlyList<DomainResponse>> GetByTicketIdAsync(Guid ticketId, CancellationToken cancellationToken = default) =>
        await context.Responses.AsNoTracking().Where(x => x.TicketId == ticketId).OrderBy(x => x.CreatedAt)
            .Select(x => new DomainResponse
            {
                Id = x.ResponseId, TicketId = x.TicketId, ResponderId = x.ResponderId,
                Message = x.Message, CreatedAt = x.CreatedAt ?? DateTime.MinValue
            }).ToListAsync(cancellationToken);
}
