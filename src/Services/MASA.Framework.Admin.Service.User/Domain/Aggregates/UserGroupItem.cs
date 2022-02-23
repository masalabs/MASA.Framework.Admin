namespace MASA.Framework.Admin.Service.User.Domain.Aggregates
{
    public class UserGroupItem : Entity<Guid>
    {
        public UserGroup UserGroup { get; private set; } = null!;

        public User user { get; private set; } = null!;
    }
}
