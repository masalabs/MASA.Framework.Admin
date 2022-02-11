namespace MASA.Framework.Admin.Contracts.Authentication.Commands.Rules;

public record EditRuleCommand : AddRuleCommand
{
    public Guid RuleId { get; set; }
}
