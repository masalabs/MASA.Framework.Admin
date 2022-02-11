namespace MASA.Framework.Admin.Contracts.Authentication.Request.Objects;

public record EditObjectRequest : Command
{
    public Guid? ObjectId { get; set; }

    public string Name { get; set; } = default!;
}
