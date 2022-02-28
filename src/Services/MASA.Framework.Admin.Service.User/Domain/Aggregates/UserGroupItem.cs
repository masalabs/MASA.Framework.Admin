namespace Masa.Framework.Admin.Service.User.Domain.Aggregates
{
    public class UserGroupItem : Entity<Guid>
    {
        public UserGroup UserGroup { get; private set; } = null!;

        public User User { get; private set; } = null!;

        public Guid UserId { get; private set; }

        public UserGroupItem(Guid userId)
        {
            UserId = userId;
        }
    }
}
