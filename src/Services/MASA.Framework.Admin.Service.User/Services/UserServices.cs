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
        var listQuery = new UserQuery.ListQuery(pageIndex, pageSize, account);
        await eventBus.PublishAsync(listQuery);
        return Success(listQuery.Result);
    }

    public async Task<ApiResultResponse<UserDetailResponse>> GetAsync(
        [FromServices] IEventBus eventBus, Guid id)
    {
        var detailQuery = new UserQuery.DetailQuery(id);
        await eventBus.PublishAsync(detailQuery);
        return Success(detailQuery.Result);
    }

    public async Task<ApiResultResponseBase> CreateAsync(
        [FromServices] IEventBus eventBus,
        [FromHeader(Name = "creator-id")] Guid creator,
        [FromBody] UserCreateRequest userCreateRequest)
    {
        await eventBus.PublishAsync(new UserCommand.CreateCommand(userCreateRequest)
        {
            Creator = creator
        });
        return Success();
    }

    public async Task<ApiResultResponseBase> DeleteAsync(
        [FromServices] IEventBus eventBus, Guid id)
    {
        await eventBus.PublishAsync(new UserCommand.DeleteCommand(id));
        return Success();
    }
}
