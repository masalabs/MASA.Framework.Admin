using MASA.Framework.Admin.Service.Authentication.Domain.Aggregate.RoleAggregate;

namespace MASA.Framework.Admin.Service.Authentication.Domain.Repositories;

public interface IRoleRepository : IRepository<Role>
{
    Task<bool> ExistAsync(string name);
}
