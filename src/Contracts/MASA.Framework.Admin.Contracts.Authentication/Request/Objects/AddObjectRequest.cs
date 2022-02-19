namespace MASA.Framework.Admin.Contracts.Authentication.Request.Objects;

public class AddObjectRequest
{
    public string Code { get; set; }

    public string Name { get; set; }

    public ObjectType ObjectType { get; set; }

    public List<ObjectItemRequest> ObjectItems { get; set; } = new();

    public AddObjectRequest(
        string code,
        string name,
        ObjectType objectType)
    {
        Code = code;
        Name = name;
        ObjectType = objectType;
    }
}
