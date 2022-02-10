namespace MASA.Framework.Admin.Service.Authentication.Services;

public class PermissionService : ServiceBase
{
    private const string PRE = "permission";

    public PermissionService(IServiceCollection services) : base(services)
    {
        App.MapGet(Routing.PermissionList, GetItemsAsync);
    }

    /// <summary>
    /// Get the set of licenses under the specified user
    /// </summary>
    /// <param name="eventBus"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    private List<PermissionItemResponse> GetItemsAsync(
        [FromServices] IEventBus eventBus,
        [FromQuery] Guid id)
    {
        return new List<PermissionItemResponse>();
    }
}
