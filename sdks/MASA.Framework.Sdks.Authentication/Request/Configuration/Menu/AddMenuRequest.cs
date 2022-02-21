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

    public AddMenuRequest(string name, string code)
    {
        Code = code;
        Name = name;
    }
}
