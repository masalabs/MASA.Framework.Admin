namespace MASA.Framework.Sdks.Authentication.Request.Configuration.Menu;

public class AddMenuRequest
{
    public string Code { get; } = default!;

    public string Name { get; }= default!;

    public string? Describe { get; set; }

    public string? Icon { get; set; }

    public Guid? ParentId { get; set; }

    public string Url { get; set; }

    public int Sort { get; set; }

    public bool Disabled { get; set; }

    public AddMenuRequest(string code, string name, string? describe, string? icon, Guid? parentId, string url, int sort, bool disabled)
    {
        Code = code;
        Name = name;
        Describe = describe;
        Icon = icon;
        ParentId = parentId;
        Url = url;
        Sort = sort;
        Disabled = disabled;
    }
}
