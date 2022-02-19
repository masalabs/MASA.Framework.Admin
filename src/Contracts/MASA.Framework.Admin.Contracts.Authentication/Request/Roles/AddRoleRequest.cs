namespace MASA.Framework.Admin.Contracts.Authentication.Request.Roles;

public class AddRoleRequest
{
    public string Name { get; set; }

    public string? Describe { get; set; }

    public int Number { get; set; }

    public State State { get; private set; }

    public List<Guid>? ChildrenIds { get; set; } = null;

    public AddRoleRequest(string name, string? describe, int number, State state, List<Guid>? childrenIds = null)
    {
        Name = name;
        Describe = describe;
        Number = number;
        State = state;
        ChildrenIds = childrenIds;
    }
}
