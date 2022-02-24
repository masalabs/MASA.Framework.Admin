using MASA.Framework.Admin.Contracts.Authentication.Old.Enum;

namespace MASA.Framework.Admin.Contracts.Authentication.Old.Response;

public class ObjectItemResponse
{
    public Guid Id { get; set; }

    public string Code { get; set; } = default!;

    public string Name { get; set; } = default!;

    public State State { get; set; } = State.Enable;

    public ObjectType ObjectType { get; set; } = ObjectType.Menu;
}