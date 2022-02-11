namespace MASA.Framework.Admin.Service.Authentication.Domain.Aggregate.ObjectAggregate;

public class Object : AuditAggregateRoot<Guid, Guid>
{
    public string Code { get; set; } = default!;

    public string Name { get; set; } = default!;

    public ObjectType ObjectType { get; set; }

    public State State { get; set; }

    private readonly List<ObjectItem> _permissionItems;

    public IReadOnlyCollection<ObjectItem> Permissions => _permissionItems;
}
