namespace MASA.Framework.Admin.Service.Authentication.Services;

public class AuthorizeService : CustomServiceBase
{
    public AuthorizeService(IServiceCollection services) : base(services)
    {
        App.MapGet(Routing.AuthorizeList, GetItemsAsync);
    }

    public ApiResultResponse<List<AuthorizeItemResponse>> GetItemsAsync(
        [FromServices] IEventBus eventBus,
        [FromQuery] Guid roleId)
    {
        var response = new List<AuthorizeItemResponse>();
        return Success(response);
    }
}
