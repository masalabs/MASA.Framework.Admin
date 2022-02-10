namespace MASA.Framework.Admin.Contracts.Authentication.Commands.Objects;

public record ChangeStateCommand : Command
{
    public Guid ObjectId { get; set; }

    public State State { get; set; }
}
