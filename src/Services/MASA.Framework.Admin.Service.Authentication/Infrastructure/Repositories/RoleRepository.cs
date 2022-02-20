namespace MASA.Framework.Admin.Service.Authentication.Infrastructure.Repositories;

public class RoleRepository : Repository<AuthenticationDbContext, Role>, IRoleRepository
{
    public RoleRepository(AuthenticationDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
    {
    }

    public async Task<Role?> FindAsync(Guid id)
    {
        return await _context.Set<Role>()
            .Include(role => role.RoleItems)
            .Include(role => role.Permissions)
            .Where(role => role.Id == id)
            .FirstOrDefaultAsync();
    }

    public new async Task<IEnumerable<Role>> GetListAsync(
        Expression<Func<Role, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await _context.Set<Role>()
            .Include(role => role.RoleItems)
            .Include(role => role.Permissions)
            .ToListAsync(cancellationToken);
    }
}
