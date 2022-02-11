namespace MASA.Framework.Admin.Contracts.Authentication.Response;

public class ObjectItemResponse
{
    public Guid Id { get; set; }

    public string Code { get; set; } = default!;

    public string Name { get; set; } = default!;

    public State State { get; set; }

    public ObjectType ObjectType { get; set; }
}
