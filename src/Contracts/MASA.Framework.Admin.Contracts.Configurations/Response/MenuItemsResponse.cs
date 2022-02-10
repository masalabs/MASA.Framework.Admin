namespace MASA.Framework.Admin.Contracts.Configurations.Response;

public class MenuItemsResponse
{
    public Guid Id { get; set; }

    public string Code { get; set; }

    public string Name { get; set; }

    public string Icon { get; set; }

    public string Url { get; set; }

    public Guid ParentId { get; set; }
}
