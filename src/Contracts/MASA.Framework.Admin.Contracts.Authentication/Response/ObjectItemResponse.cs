namespace MASA.Framework.Admin.Contracts.Authentication.Response;

public class ObjectItemResponse
{
    public Guid Id { get; set; }

    [Required]
    public string Code { get; set; } = default!;

    [Required]
    public string Name { get; set; } = default!;

    public State State { get; set; } = State.Enable;

    public ObjectType ObjectType { get; set; } = ObjectType.Menu;

    public ObjectItemResponse Copy() => (ObjectItemResponse)MemberwiseClone();
}
