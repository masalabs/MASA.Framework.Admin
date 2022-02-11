namespace MASA.Framework.Admin.Contracts.Authentication.Commands.Rules;

public record AddRuleCommand : Command
{
    public string Name { get; set; } = default!;

    public string Describe { get; set; } = default!;

    public int Number { get; set; }

    public State State { get; set; }
}
