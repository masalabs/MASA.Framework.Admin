namespace MASA.Framework.Admin.Contracts.Authentication.Commands.Objects;

public record EditCommand : Command
{
    public Guid? ObjectId { get; set; }

    public string Name { get; set; } = default!;
}
