namespace Masa.Framework.Admin.Web.Base.Global;

public class NavModel
{
    public Guid Id { get; set; }

    public Guid? ParentId { get; set; }

    public NavModel? ParentNav { get; set; }

    public string? _href { get; set; }

    public string? Href
    {
        get => _href == "" ? null : _href;
        set => _href = value;
    }

    public string? _icon { get; set; }

    public string? Icon
    {
        get => _icon == "" ? null : _icon;
        set => _icon = value;
    }

    public string? InheritIcon { get; set; }

    public string Title { get; set; }

    public string FullTitle { get; set; }

    public int Sort { get; set; }

    public string Code { get; set; }

    public bool Hide { get; set; }

    public bool Active { get; set; }

    public bool OnlyJump { get; set; }

    public NavModel[]? Children { get; set; }

    public NavModel(Guid id, string code,string? href, string? icon, string title, int sort, bool onlyJump,bool hide, Guid? parentId,NavModel? parentNav, NavModel[]? children)
    {
        Id = id;
        Code = code;
        Href = href;
        Icon = icon;
        Title = title;
        Sort = sort;
        OnlyJump = onlyJump;
        Hide = hide;
        FullTitle = title;
        ParentId = parentId;
        ParentNav = parentNav;
        Children = children;
    }
}
