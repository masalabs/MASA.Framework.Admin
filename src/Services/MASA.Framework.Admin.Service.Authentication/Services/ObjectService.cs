namespace MASA.Framework.Admin.Service.Authentication.Services;

public class ObjectService : CustomServiceBase
{
    public ObjectService(IServiceCollection services) : base(services)
    {
        App.MapGet(Routing.ObjectList, GetItemsAsync);
        App.MapGet(Routing.ObjectList, GetItemsAsync);
        App.MapGet(Routing.ObjectList, GetItemsAsync);
    }

    private ApiResultResponse<PaginatedItemsViewModel<ObjectItemResponse>> GetItemsAsync(
        [FromServices] IEventBus eventBus,
        [FromQuery] int pageIndex = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] ObjectType? type = null,
        [FromQuery] string name = "")
    {
        var response = new PaginatedItemsViewModel<ObjectItemResponse>(pageIndex, pageSize, 0, new List<ObjectItemResponse>());
        return Success(response);
    }
}
