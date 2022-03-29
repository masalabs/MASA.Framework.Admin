namespace Masa.Framework.Admin.Service.User.Infrastructure.Repositories;

public class UserGroupRepository : Repository<UserDbContext, UserGroup>, IUserGroupRepository
{
    public UserGroupRepository(UserDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
    {

    }

    public async Task<UserGroup?> GetByIdAsync(Guid Id)
    {
        return await Context.Set<UserGroup>().Where(a => a.Id == Id).Include(u => u.UserGroupPermissions)
            .Include(b => b.UserGroupItems).ThenInclude(u => u.User).FirstOrDefaultAsync();
    }
}

