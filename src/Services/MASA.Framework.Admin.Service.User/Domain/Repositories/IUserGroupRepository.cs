namespace MASA.Framework.Admin.Service.User.Domain.Repositories
{
    public interface IUserGroupRepository : IRepository<UserGroup>
    {
        Task<UserGroup> GetByIdAsync(Guid Id);
    }
}
