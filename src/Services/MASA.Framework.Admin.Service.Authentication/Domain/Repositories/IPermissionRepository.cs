namespace MASA.Framework.Admin.Service.Authentication.Domain.Repositories;

public interface IPermissionRepository : IRepository<Permission>
{
    Task<bool> AnyAsync(Expression<Func<Permission, bool>> condition);
}
