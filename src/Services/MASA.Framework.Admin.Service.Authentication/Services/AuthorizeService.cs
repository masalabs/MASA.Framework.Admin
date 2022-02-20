namespace MASA.Framework.Admin.Service.Authentication.Services;

public class AuthorizeService : ServiceBase
{
    public AuthorizeService(IServiceCollection services) : base(services)
    {
        App.MapGet(Routing.AuthorizeList, GetItemsAsync);
    }

    private List<AuthorizeItemResponse> GetItemsAsync(
        [FromServices] IEventBus eventBus,
        [FromQuery] Guid roleId)
    {
        return new List<AuthorizeItemResponse>();
    }
}
