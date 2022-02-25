namespace MASA.Framework.Admin.Service.User.Domain.Aggregates
{
    public class UserGroup : AuditAggregateRoot<Guid, Guid>
    {
        public string Name { get; private set; }

        public string Code { get; private set; }

        public string Describtion { get; private set; }

        private UserGroup()
        {

        }

        public UserGroup(string name, string code, string describtion)
        {
            Name = name;
            Code = code;
            Describtion = describtion;
        }
    }
}
