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

    public async Task<ApiResultResponse<PaginatedItemResponse<UserItemResponse>>> GetItemsAsync(
        [FromServices] IEventBus eventBus,
        [FromQuery] int pageIndex = Config.DEFAULT_PAGE_INDEX,
        [FromQuery] int pageSize = Config.DEFAULT_PAGE_SIZE,
        [FromQuery] string account = "",
        [FromQuery] int state = -1)
    {
        var listQuery = new ListQuery(pageIndex, pageSize, account);
        await eventBus.PublishAsync(listQuery);
        var response = new PaginatedItemResponse<UserItemResponse>(pageIndex, pageSize, listQuery.Total, listQuery.Result);
        return Success(response);
    }

    public async Task<ApiResultResponse<UserDetailResponse>> GetAsync(
        [FromServices] IEventBus eventBus,
        [FromQuery] Guid id)
    {
        var detailQuery = new DetailQuery(id);
        await eventBus.PublishAsync(detailQuery);
        return Success(detailQuery.Result);
    }

    public async Task<ApiResultResponseBase> CreateAsync(
        [FromServices] IEventBus eventBus,
        [FromQuery] Guid createUserId,
        [FromBody] UserCreateRequest userCreateRequest)
    {
        await eventBus.PublishAsync(new CreateCommand(userCreateRequest)
        {
            LoginUserId = createUserId
        });
        return Success();
    }

    public async Task<ApiResultResponseBase> DeleteAsync(
        [FromServices] IEventBus eventBus,
        [FromQuery] Guid id)
    {
        await eventBus.PublishAsync(new DeleteCommand(id));
        return Success();
    }
}
