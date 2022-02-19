namespace MASA.Framework.Admin.Contracts.Authentication.Request.Objects;

public class EditObjectRequest
{
    public Guid ObjectId { get; set; }

    public string Name { get; set; }

    public EditObjectRequest(Guid objectId, string name)
    {
        ObjectId = objectId;
        Name = name;
    }
}
