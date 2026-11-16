using Lab10_NagelyArpi.Domain.Repositories;
using Lab10_NagelyArpi.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using DomainRole = Lab10_NagelyArpi.Domain.Entities.Role;

namespace Lab10_NagelyArpi.Infrastructure.Adapters.Repositories;

public sealed class RoleRepository(TicketeraBdContext context) : IRoleRepository
{
    public async Task<DomainRole?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var role = await context.Roles.AsNoTracking().FirstOrDefaultAsync(x => x.RoleId == id, cancellationToken);
        return role is null ? null : Map(role);
    }

    public async Task<IReadOnlyList<DomainRole>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await context.Roles.AsNoTracking().OrderBy(x => x.RoleName)
            .Select(x => new DomainRole { Id = x.RoleId, Name = x.RoleName }).ToListAsync(cancellationToken);

    public Task AddAsync(DomainRole role, CancellationToken cancellationToken = default)
    {
        context.Roles.Add(new Lab10_NagelyArpi.Infrastructure.Context.Role
        {
            RoleId = role.Id,
            RoleName = role.Name
        });
        return Task.CompletedTask;
    }

    private static DomainRole Map(Lab10_NagelyArpi.Infrastructure.Context.Role role) => new() { Id = role.RoleId, Name = role.RoleName };
}
