namespace MASA.Framework.Admin.Service.Authentication.Domain.Aggregate.ObjectAggregate;

public class Object : AuditAggregateRoot<Guid, Guid>
{
    public string Code { get; private set; }

    public string Name { get; private set; }

    public ObjectType ObjectType { get; private set; }

    public State State { get; private set; }

    private readonly List<ObjectItem> _permissionItems;

    public IReadOnlyCollection<ObjectItem> Permissions => _permissionItems;

    private Object()
    {
        _permissionItems = new();
        State = State.Enable;
        Code = "";
        Name = "";
    }

    public Object(Guid creator, string code, string name, State state,ObjectType objectType) : this()
    {
        Creator = creator;
        Modifier = creator;
        Code = code;
        Name = name;
        State = state;
        ObjectType = objectType;
    }

    public void Update(string name, State state)
    {
        this.Name = name;
        this.State = state;
        this.ModificationTime = DateTime.Now;
    }

    public void AddPermission(string name, string objectIdentifies, string action, PermissionType permissionType)
    {
        _permissionItems.Add(new ObjectItem(name, objectIdentifies, action, permissionType));
    }
}
