namespace MASA.Framework.Admin.Contracts.Authentication.Request.Objects;

public class AddObjectRequest
{
    public string Code { get; set; } = default!;

    public string Name { get; set; } = default!;

    public ObjectType ObjectType { get; set; }
}
