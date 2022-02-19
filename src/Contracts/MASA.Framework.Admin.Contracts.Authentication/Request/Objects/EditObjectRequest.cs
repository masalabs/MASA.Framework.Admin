namespace MASA.Framework.Admin.Contracts.Authentication.Request.Objects;

public class EditObjectRequest
{
    public Guid ObjectId { get; set; }

    public string Name { get; set; }

    public State State { get; set; }

    public EditObjectRequest(Guid objectId, string name,State state)
    {
        ObjectId = objectId;
        Name = name;
        State = state;
    }
}
