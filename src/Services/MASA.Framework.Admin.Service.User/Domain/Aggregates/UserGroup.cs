namespace MASA.Framework.Admin.Service.User.Domain.Aggregates
{
    public class UserGroup : AuditAggregateRoot<Guid, Guid>
    {
        public string Name { get; private set; }

        public string Code { get; private set; }

        public string Describtion { get; private set; }
    }
}
