namespace MASA.Framework.Admin.Service.Authentication.Services;

public class ObjectService : CustomServiceBase
{
    public ObjectService(IServiceCollection services) : base(services)
    {
        App.MapGet(Routing.ObjectList, GetItemsAsync);
        App.MapPost(Routing.OperateObject, AddAsync);
        App.MapPut(Routing.OperateObject, EditAsync);
        App.MapPut(Routing.ObjectChangeState, EditAsync);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="eventBus"></param>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize">When the page number is -1, no pagination is performed at this time，default: 20</param>
    /// <param name="type"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public ApiResultResponse<PaginatedItemResponse<ObjectItemResponse>> GetItemsAsync(
        [FromServices] IEventBus eventBus,
        [FromQuery] int pageIndex = Config.DEFAULT_PAGE_INDEX,
        [FromQuery] int pageSize = Config.DEFAULT_PAGE_SIZE,
        [FromQuery] int type = -1,
        [FromQuery] string name = "")
    {
        var response = new PaginatedItemResponse<ObjectItemResponse>(pageIndex, pageSize, 0, new List<ObjectItemResponse>());
        return Success(response);
    }

    public ApiResultResponseBase AddAsync(
        [FromServices] IEventBus eventBus,
        [FromBody] AddCommand command)
    {
        return Success();
    }

    public ApiResultResponseBase EditAsync(
        [FromServices] IEventBus eventBus,
        [FromBody] EditCommand command)
    {
        return Success();
    }

    public ApiResultResponseBase ChangeStateAsync(
        [FromServices] IEventBus eventBus,
        [FromBody] ChangeStateCommand command)
    {
        return Success();
    }
}
