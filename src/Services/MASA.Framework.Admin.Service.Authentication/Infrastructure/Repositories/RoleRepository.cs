using MASA.BuildingBlocks.Data.UoW;

namespace MASA.Framework.Admin.Service.Authentication.Infrastructure.Repositories;

public class RoleRepository : Repository<AuthenticationDbContext, Role>, IRoleRepository
{

    public RoleRepository(AuthenticationDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
    {
    }

    public async Task<bool> ExistAsync(string name)
    {
        return await _context.Set<Role>().AnyAsync(role => role.Name == name);
    }

    public List<Role> GetList()
    {
        return _context.Set<Role>().ToList();
    }
}
