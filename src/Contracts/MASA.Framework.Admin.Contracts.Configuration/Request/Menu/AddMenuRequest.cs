namespace MASA.Framework.Admin.Contracts.Configuration.Request.Menu;

public class AddMenuRequest
{
    public string Code { get; private set; }

    public string Name { get; set; }

    public string? Describe { get; set; }

    public string? Icon { get; set; }

    public Guid? ParentId { get; set; }

    public string? ParentName { get; set; }

    public string Url { get; set; }

    public int Sort { get; set; }

    public bool Disabled { get; set; }

    public AddMenuRequest(string name, string code,string url, int sort,bool disabled)
    {
        Code = code;
        Name = name;
        Sort = sort;
        Disabled = disabled;
        Url = url;
    }
}
