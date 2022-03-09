namespace Masa.Framework.Admin.Service.Authentication.Domain.Repositories;

public interface IPermissionRepository : IRepository<Permission, Guid>
{
    Task<bool> AnyAsync(Expression<Func<Permission, bool>> condition);
}
