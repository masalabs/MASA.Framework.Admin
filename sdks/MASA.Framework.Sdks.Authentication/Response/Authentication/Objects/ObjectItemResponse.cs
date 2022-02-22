namespace MASA.Framework.Sdks.Authentication.Response.Authentication.Objects;

public class ObjectItemResponse
{
    public Guid Id { get; set; }

    public string Code { get; set; } = default!;

    public string Name { get; set; } = default!;

    public bool Enable { get; set; }

    public int ObjectType { get; set; }

    public string ObjectTypeName { get; set; } = default!;
}
