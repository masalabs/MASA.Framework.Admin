namespace MASA.Framework.Sdks.Authentication.Request.Authentication.Objects;

public class EditObjectRequest
{
    public Guid ObjectId { get; set; }

    public string Name { get; set; } = default!;
}
