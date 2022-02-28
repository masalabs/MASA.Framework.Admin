namespace Masa.Framework.Admin.Web.Base.Global;

public class NavModel
{
    public Guid Id { get; set; }

    public Guid? ParentId { get; set; }

    public string? Href { get; set; }

    public string? Icon { get; set; }

    public string? ParentIcon { get; set; }

    public string Title { get; set; }

    public string FullTitle { get; set; }

    public int Sort { get; set; }

    public string Code { get; set; }

    public bool Hide { get; set; }

    public bool Active { get; set; }

    public NavModel[]? Children { get; set; }

    public NavModel(Guid id, string code,string? href, string? icon, string title, int sort, Guid? parentId, NavModel[]? children)
    {
        Id = id;
        Code = code;
        Href = href;
        Icon = icon;
        ParentIcon = icon;
        Title = title;
        Sort = sort;
        ParentId = parentId;
        FullTitle = title;
        Children = children;
    }
}
