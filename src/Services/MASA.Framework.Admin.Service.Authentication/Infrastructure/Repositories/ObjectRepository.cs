namespace MASA.Framework.Admin.Service.Authentication.Infrastructure.Repositories;

public class PermissionRepository :
    Repository<AuthenticationDbContext, Permission>,
    IPermissionRepository
{
    public PermissionRepository(AuthenticationDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
    {
    }

    public Task<bool> AnyAsync(Expression<Func<Permission, bool>> condition)
    {
        return _context.Set<Permission>().AnyAsync(condition);
    }
}
