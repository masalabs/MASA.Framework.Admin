namespace MASA.Framework.Admin.Contracts.Authentication.Request.Roles;

public class EditRoleRequest
{
    public Guid RuleId { get; set; }

    public string Name { get; set; }

    public int Number { get; set; }

    public string? Describe { get; set; }

    public State State { get; set; }

    public EditRoleRequest(Guid ruleId, string name, int number, string? describe, State state)
    {
        RuleId = ruleId;
        Name = name;
        Number = number;
        Describe = describe;
        State = state;
    }
}
