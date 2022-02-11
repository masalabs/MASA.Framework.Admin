namespace MASA.Framework.Admin.Service.Authentication.Services;

public class ObjectService : CustomServiceBase
{
    public ObjectService(IServiceCollection services) : base(services)
    {
        App.MapGet(Routing.ObjectList, GetItemsAsync);
        App.MapGet(Routing.ObjectEdit, EditAsync);
        App.MapGet(Routing.ObjectEdit, EditAsync);
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

    public ApiResultResponseBase AddAsync(
        [FromServices] IEventBus eventBus,
        [FromBody] AddCommand command)
    {
        return Success("添加资源成功");
    }

    public ApiResultResponseBase EditAsync(
        [FromServices] IEventBus eventBus,
        [FromBody] EditCommand command)
    {
        return Success("编辑资源成功");
    }
}
