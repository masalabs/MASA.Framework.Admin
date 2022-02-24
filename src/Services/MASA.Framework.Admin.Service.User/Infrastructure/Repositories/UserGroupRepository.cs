namespace MASA.Framework.Admin.Service.User.Infrastructure.Repositories
{
    public class UserGroupRepository : Repository<UserDbContext, UserGroup>, IUserGroupRepository
    {
        public UserGroupRepository(UserDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }
    }
}
