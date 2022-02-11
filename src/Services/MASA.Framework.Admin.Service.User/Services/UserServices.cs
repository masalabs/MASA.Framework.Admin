namespace MASA.Framework.Admin.Service.User.Services;

public class UserServices : CustomServiceBase
{
    public UserServices(IServiceCollection services) : base(services)
    {
        App.MapGet(Routing.UserList, GetItemsAsync);
        App.MapGet(Routing.UserDetail, GetAsync);
        App.MapPost(Routing.OperateUser, CreateAsync);
        App.MapDelete(Routing.OperateUser, DeleteAsync);
    }

    public ApiResultResponse<PaginatedItemResponse<UserItemResponse>> GetItemsAsync(
        [FromServices] IEventBus eventBus,
        [FromQuery] int pageIndex = Config.DEFAULT_PAGE_INDEX,
        [FromQuery] int pageSize = Config.DEFAULT_PAGE_SIZE,
        [FromQuery] string account = "",
        [FromQuery] int state = -1)
    {
        var listQuery = new ListQuery(pageIndex, pageSize, account);
        eventBus.PublishAsync(listQuery);
        var response = new PaginatedItemResponse<UserItemResponse>(pageIndex, pageSize, listQuery.Total, listQuery.Result);
        return Success(response);
    }

    public ApiResultResponse<UserDetailResponse> GetAsync(
        [FromServices] IEventBus eventBus,
        [FromQuery] Guid id)
    {
        var detailQuery = new DetailQuery(id);
        eventBus.PublishAsync(detailQuery);
        return Success(detailQuery.Result);
    }

    public ApiResultResponseBase CreateAsync(
        [FromServices] IEventBus eventBus,
        [FromBody] UserCreateRequest userCreateRequest)
    {
        eventBus.PublishAsync(new CreateCommand(userCreateRequest));
        return Success();
    }

    public ApiResultResponseBase DeleteAsync(
        [FromServices] IEventBus eventBus,
        [FromQuery] Guid id)
    {
        eventBus.PublishAsync(new DeleteCommand(id));
        return Success();
    }
}
