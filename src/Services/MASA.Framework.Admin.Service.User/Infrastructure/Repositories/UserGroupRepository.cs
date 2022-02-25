namespace MASA.Framework.Admin.Service.User.Infrastructure.Repositories
{
    public class UserGroupRepository : Repository<UserDbContext, UserGroup>, IUserGroupRepository
    {
        public UserGroupRepository(UserDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {

        }

        public async Task<UserGroup> GetByIdAsync(Guid Id)
        {
            return await _context.Set<UserGroup>().Where(a => a.Id == Id)
                .Include(b => b.UserGroupItems).SingleOrDefaultAsync();
        }
    }
}
