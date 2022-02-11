namespace MASA.Framework.Admin.Contracts.Configuration.Request.Menu;

public class AddMenuRequest
{
    public string Code { get; }

    public string Name { get; }

    public string? Describe { get; set; }

    public string? Icon { get; set; }

    public Guid? ParentId { get; }

    public string ParentName { get; }

    public string? Url { get; set; }

    public int Sort { get; }

    public AddMenuRequest(string code, string name, int sort, Guid? parentId, string? parentName)
    {
        Code = code;
        Name = name;
        ParentId = parentId;
        ParentName = parentName ?? string.Empty;
        Sort = sort;
    }
}
