namespace MASA.Framework.Admin.Service.Authentication.Domain.Repositories;

public interface IRoleRepository : IRepository<Role>
{
    Task<Role?> FindAsync(Guid id);
}
