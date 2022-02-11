namespace MASA.Framework.Admin.Contracts.Configuration.Request.Menu;

public class EditMenuRequest
{
    public Guid MenuId { get; set; }

    public string Name { get; }

    public string? Describe { get; set; }

    public string? Icon { get; set; }

    public Guid? ParentId { get; }

    public string ParentName { get; }

    public string? Url { get; set; }

    public int Sort { get; }

    public EditMenuRequest(Guid menuId, string name, int sort, Guid? parentId, string? parentName)
    {
        MenuId = menuId;
        Name = name;
        ParentId = parentId;
        ParentName = parentName ?? string.Empty;
        Sort = sort;
    }
}
