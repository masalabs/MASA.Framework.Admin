namespace MASA.Framework.Admin.Service.User.Domain.Aggregates
{
    public class UserGroup : AggregateRoot<Guid>
    {
        public string Name { get; private set; }

        public string Code { get; private set; }

        public string Describtion { get; private set; }
    }
}
