namespace Masa.Framework.Sdks.Authentication.Request.Configuration.Menu;

public class AddMenuRequest
{
    public string Code { get; } = default!;

    public string Name { get; }= default!;

    public string? Description { get; set; }

    public string? Icon { get; set; }

    public Guid? ParentId { get; set; }

    public string Url { get; set; }

    public int Sort { get; set; }

    public bool Enabled { get; set; }

    public bool OnlyJump { get; set; }

    public AddMenuRequest(string code, string name, string? description, string? icon, Guid? parentId, string url, int sort, bool enabled, bool onlyJump)
    {
        Code = code;
        Name = name;
        Description = description;
        Icon = icon;
        ParentId = parentId;
        Url = url;
        Sort = sort;
        Enabled = enabled;
        OnlyJump = onlyJump;
    }
}
