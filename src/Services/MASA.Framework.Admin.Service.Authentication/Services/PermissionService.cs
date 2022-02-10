using MASA.Framework.Admin.Contracts.Authentication.Response;

namespace MASA.Framework.Admin.Service.Authentication.Services;

public class PermissionService : ServiceBase
{
    public PermissionService(IServiceCollection services) : base(services)
    {
        App.MapGet("/api/permission/items", GetItemsAsync);
    }

    private List<PermissionItemResponse> GetItemsAsync(
        [FromServices] IEventBus eventBus)
    {
        return new List<PermissionItemResponse>();
    }
}
