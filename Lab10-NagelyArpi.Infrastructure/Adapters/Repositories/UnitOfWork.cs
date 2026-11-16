using Lab10_NagelyArpi.Domain.Repositories;
using Lab10_NagelyArpi.Infrastructure.Context;

namespace Lab10_NagelyArpi.Infrastructure.Adapters.Repositories;

public sealed class UnitOfWork(TicketeraBdContext context) : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        context.SaveChangesAsync(cancellationToken);
}
