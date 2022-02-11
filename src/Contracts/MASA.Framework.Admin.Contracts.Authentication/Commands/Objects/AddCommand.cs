namespace MASA.Framework.Admin.Contracts.Authentication.Commands.Objects;

public record AddCommand : Command
{
    public string Code { get; set; } = default!;

    public string Name { get; set; } = default!;

    public ObjectType ObjectType { get; set; }
}
