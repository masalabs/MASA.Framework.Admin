namespace Masa.Framework.Admin.Configuration.Domain.Aggregate;

public class Menu : AuditAggregateRoot<Guid, Guid>, ISoftDelete
{
    public string Code { get; private set; } = default!;

    public string Name { get; private set; } = default!;

    public string? Description { get; private set; }

    public string Icon { get; private set; }

    public Guid? ParentId { get; private set; }

    public string Url { get; private set; }

    public int Sort { get; private set; }

    public bool Enabled { get; private set; } = true;

    public bool OnlyJump { get; private set; }

    public bool IsDeleted { get; private set; }

    public Menu(
        Guid creator,
        string code,
        string name,
        string? url,
        string? icon,
        string? description,
        Guid? parentId,
        int sort,
        bool enabled,
        bool onlyJump)
    {
        Creator = creator;
        Modifier = creator;
        Code = code;
        Name = name;
        Url = url ?? string.Empty;
        Sort = sort;
        Enabled = enabled;
        Icon = icon ?? string.Empty;
        Description = description;
        ParentId = parentId;
        OnlyJump = onlyJump;
    }

    public void Update(
        Guid creator,
        string name,
        string? url,
        string? icon,
        string? description,
        Guid? parentId,
        int sort,
        bool enabled,
        bool onlyJump)
    {
        Name = name;
        Url = url ?? string.Empty;
        Sort = sort;
        Enabled = enabled;
        Icon = icon ?? string.Empty;
        Description = description;
        ParentId = parentId;
        OnlyJump = onlyJump;
        Modifier = creator;
        ModificationTime = DateTime.Now;
    }
}
