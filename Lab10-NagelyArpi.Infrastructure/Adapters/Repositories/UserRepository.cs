using Lab10_NagelyArpi.Domain.Repositories;
using Lab10_NagelyArpi.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using DomainUser = Lab10_NagelyArpi.Domain.Entities.User;

namespace Lab10_NagelyArpi.Infrastructure.Adapters.Repositories;

public sealed class UserRepository(TicketeraBdContext context) : IUserRepository
{
    public async Task<DomainUser?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == id, cancellationToken);
        return user is null ? null : Map(user);
    }

    public async Task<DomainUser?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        var user = await context.Users.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Username == username, cancellationToken);
        return user is null ? null : Map(user);
    }

    public async Task<DomainUser?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var user = await context.Users.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
        return user is null ? null : Map(user);
    }

    public async Task<IReadOnlyList<DomainUser>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await context.Users.AsNoTracking().OrderBy(x => x.Username).Select(x => new DomainUser
        {
            Id = x.UserId,
            Username = x.Username,
            Email = x.Email,
            PasswordHash = x.PasswordHash
        }).ToListAsync(cancellationToken);

    public Task AddAsync(DomainUser user, DateTime createdAt, CancellationToken cancellationToken = default)
    {
        context.Users.Add(new Lab10_NagelyArpi.Infrastructure.Context.User
        {
            UserId = user.Id,
            Username = user.Username,
            Email = user.Email,
            PasswordHash = user.PasswordHash,
            CreatedAt = createdAt
        });
        return Task.CompletedTask;
    }

    public async Task<bool> UpdateAsync(DomainUser user, CancellationToken cancellationToken = default)
    {
        var entity = await context.Users.FirstOrDefaultAsync(x => x.UserId == user.Id, cancellationToken);
        if (entity is null) return false;
        entity.Username = user.Username;
        entity.Email = user.Email;
        if (!string.IsNullOrWhiteSpace(user.PasswordHash)) entity.PasswordHash = user.PasswordHash;
        return true;
    }

    private static DomainUser Map(Lab10_NagelyArpi.Infrastructure.Context.User user) => new()
    {
        Id = user.UserId,
        Username = user.Username,
        Email = user.Email,
        PasswordHash = user.PasswordHash
    };
}
