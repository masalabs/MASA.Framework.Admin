namespace MASA.Framework.Admin.Service.Authentication.Domain.Aggregates.ObjectAggregate;

public class Object: AuditAggregateRoot<Guid, Guid>
{
    public string Code { get; private set; }

    public string Name { get; private set; }

    public ObjectType ObjectType { get; private set; }

    public bool Enable { get; private set; }

    private Object()
    {
        Code = "";
        Name = "";
        Enable = true;
    }

    public Object(Guid creator, string code, string name, ObjectType objectType) : this()
    {
        Creator = creator;
        Modifier = creator;
        Code = code;
        Name = name;
        ObjectType = objectType;
    }

    public void Update(string name)
    {
        this.Name = name;
        this.ModificationTime = DateTime.Now;
    }
}

