namespace MASA.Framework.Sdks.Authentication.Request.Authentication.Objects;

public class AddObjectRequest
{
    public string Code { get; set; } = default!;

    public string Name { get; set; } = default!;

    public int ObjectType { get; set; }
}
