using Lab10_NagelyArpi.Domain.Repositories;
using Lab10_NagelyArpi.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using DomainUserRole = Lab10_NagelyArpi.Domain.Entities.UserRole;

namespace Lab10_NagelyArpi.Infrastructure.Adapters.Repositories;

public sealed class UserRoleRepository(TicketeraBdContext context) : IUserRoleRepository
{
    public async Task<DomainUserRole> AddAsync(DomainUserRole userRole, CancellationToken cancellationToken = default)
    {
        var entity = new Lab10_NagelyArpi.Infrastructure.Context.UserRole
        {
            UserId = userRole.UserId, RoleId = userRole.RoleId, AssignedAt = userRole.AssignedAt
        };
        context.UserRoles.Add(entity);
        return userRole;
    }

    public async Task<IReadOnlyList<DomainUserRole>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default) =>
        await context.UserRoles.AsNoTracking().Where(x => x.UserId == userId)
            .Select(x => new DomainUserRole
            {
                UserId = x.UserId, RoleId = x.RoleId, AssignedAt = x.AssignedAt ?? DateTime.MinValue
            }).ToListAsync(cancellationToken);

    public async Task<bool> RemoveAsync(Guid userId, Guid roleId, CancellationToken cancellationToken = default)
    {
        var entity = await context.UserRoles.FindAsync([userId, roleId], cancellationToken);
        if (entity is null) return false;
        context.UserRoles.Remove(entity);
        return true;
    }
}
