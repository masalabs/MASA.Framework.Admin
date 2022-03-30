namespace Masa.Framework.Admin.Service.Authentication.Infrastructure.Repositories;

public class PermissionRepository :
    Repository<AuthenticationDbContext, Permission, Guid>,
    IPermissionRepository
{
    public PermissionRepository(AuthenticationDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
    {
    }

    public Task<bool> AnyAsync(Expression<Func<Permission, bool>> condition)
    {
        return Context.Set<Permission>().AnyAsync(condition);
    }
}
