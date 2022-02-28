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

    private Menu()
    {
        CreationTime = DateTime.UtcNow;
        ModificationTime = DateTime.UtcNow;
    }

    public Menu(
        Guid creator,
        string code,
        string name,
        string? url,
        string? icon,
        string? describe,
        Guid? parentId,
        int sort,
        bool disabled) : this()
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
    }

    public void Update(
        Guid creator,
        string name,
        string? url,
        string? icon,
        string? describe,
        Guid? parentId,
        int sort,
        bool disabled)
    {
        Name = name;
        Url = url ?? string.Empty;
        Sort = sort;
        Enable = !disabled;
        Icon = icon ?? string.Empty;
        Describe = describe;
        ParentId = parentId;
        Modifier = creator;
        ModificationTime = DateTime.Now;
    }
}
