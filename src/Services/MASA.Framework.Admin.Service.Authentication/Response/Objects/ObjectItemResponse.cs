namespace MASA.Framework.Admin.Service.Authentication.Response.Objects;

public class ObjectItemResponse
{
    public Guid Id { get; set; }

    public string Code { get; set; } = default!;

    public string Name { get; set; } = default!;

    public bool Enable { get; set; }

    public ObjectType ObjectType { get; set; }

    public string ObjectTypeName => ObjectType == ObjectType.Menu ? "menu" : "operator";
}
