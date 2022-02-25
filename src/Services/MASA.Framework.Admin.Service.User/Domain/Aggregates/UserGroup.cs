namespace MASA.Framework.Admin.Service.User.Domain.Aggregates
{
    public class UserGroup : AuditAggregateRoot<Guid, Guid>
    {
        public string Name { get; private set; }

        public string Code { get; private set; }

        public string Describtion { get; private set; }

        private List<UserGroupItem> userGroupItems = new();

        public IReadOnlyCollection<UserGroupItem> UserGroupItems => userGroupItems;

        private UserGroup()
        {

        }

        public UserGroup(string name, string code, string describtion)
        {
            Name = name;
            Code = code;
            Describtion = describtion;
        }

        public void AddUser(Guid userId)
        {
            if (!userGroupItems.Any(r => r.UserId == userId))
            {
                userGroupItems.Add(new UserGroupItem(userId));
            }
        }

        public void RemoveUser(Guid userId)
        {
            if (userGroupItems.Any(r => r.UserId == userId))
            {
                userGroupItems.RemoveAll(r => r.UserId == userId);
            }
        }
    }
}
