using MASA.Framework.Admin.Contracts.Base.Enum;

namespace MASA.Framework.Admin.Configuration.Domain.Aggregate;

public class Menu : AuditAggregateRoot<Guid, Guid>
{
    public string Code { get; private set; } = default!;

    public string Name { get; private set; } = default!;

    public string? Describe { get; set; } = default!;

    public string Icon { get; set; } = default!;

    public Guid? ParentId { get; private set; }

    public string ParentName { get; private set; } = default!;

    public string Url { get; set; } = default!;

    public int Sort { get; private set; }

    public State State { get; private set; }

    private Menu()
    {
        State = State.Enable;
        Describe = String.Empty;
        Icon = String.Empty;
        Url = String.Empty;
    }

    public Menu(Guid creator, string code, string name, int sort, Guid? parentId, string? parentName) : this()
    {
        Creator = creator;
        Modifier = creator;
        Code = code;
        Name = name;
        ParentId = parentId;
        ParentName = parentName ?? string.Empty;
        Sort = sort;
    }

    public void Update(Guid creator, string name, int sort, Guid? parentId, string? parentName)
    {
        Modifier = creator;
        ModificationTime = DateTime.Now;
        Name = name;
        ParentId = parentId;
        ParentName = parentName ?? string.Empty;
        Sort = sort;
    }
}
