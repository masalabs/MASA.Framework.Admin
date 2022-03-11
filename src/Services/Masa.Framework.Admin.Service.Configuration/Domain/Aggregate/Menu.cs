namespace Masa.Framework.Admin.Configuration.Domain.Aggregate;

public class Menu : AuditAggregateRoot<Guid, Guid>
{
    public string Code { get; private set; } = default!;

    public string Name { get; private set; } = default!;

    public string? Describe { get; private set; }

    public string Icon { get; private set; }

    public Guid? ParentId { get; private set; }

    public string Url { get; private set; }

    public int Sort { get; private set; }

    public bool Enable { get; private set; }

    public bool OnlyJump { get; private set; }

    public Menu(
        Guid creator,
        string code,
        string name,
        string? url,
        string? icon,
        string? describe,
        Guid? parentId,
        int sort,
        bool disabled,
        bool onlyJump)
    {
        Creator = creator;
        Modifier = creator;
        Code = code;
        Name = name;
        Url = url ?? string.Empty;
        Sort = sort;
        Enable = !disabled;
        Icon = icon ?? string.Empty;
        Describe = describe;
        ParentId = parentId;
        OnlyJump = onlyJump;
    }

    public void Update(
        Guid creator,
        string name,
        string? url,
        string? icon,
        string? describe,
        Guid? parentId,
        int sort,
        bool disabled,
        bool onlyJump)
    {
        Name = name;
        Url = url ?? string.Empty;
        Sort = sort;
        Enable = !disabled;
        Icon = icon ?? string.Empty;
        Describe = describe;
        ParentId = parentId;
        OnlyJump = onlyJump;
        Modifier = creator;
        ModificationTime = DateTime.Now;
    }
}
