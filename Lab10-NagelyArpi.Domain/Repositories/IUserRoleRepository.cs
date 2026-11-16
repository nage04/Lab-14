using Lab10_NagelyArpi.Domain.Entities;

namespace Lab10_NagelyArpi.Domain.Repositories;

public interface IUserRoleRepository
{
    Task<UserRole> AddAsync(UserRole userRole, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<UserRole>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<bool> RemoveAsync(Guid userId, Guid roleId, CancellationToken cancellationToken = default);
}
