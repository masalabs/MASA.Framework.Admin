using MASA.Framework.Admin.Service.User.Application.Users.Commands;

namespace MASA.Framework.Admin.Service.User.Services;

public class UserServices : CustomServiceBase
{
    public UserServices(IServiceCollection services) : base(services)
    {
        App.MapGet(Routing.UserList, () => GetItemsAsync);
        App.MapGet(Routing.UserDetail, () => GetAsync);
        App.MapPost(Routing.OperateUser, () => CreateAsync);
        App.MapDelete(Routing.OperateUser, () => DeleteAsync);
    }

    public ApiResultResponse<PaginatedItemResponse<UserItemResponse>> GetItemsAsync(
        [FromServices] IEventBus eventBus,
        [FromQuery] UserType type,
        [FromQuery] int pageIndex = Config.DEFAULT_PAGE_INDEX,
        [FromQuery] int pageSize = Config.DEFAULT_PAGE_SIZE,
        [FromQuery] string account = "",
        [FromQuery] int state = -1)
    {
        var response = new PaginatedItemResponse<UserItemResponse>(pageIndex, pageSize, 0, new List<UserItemResponse>());
        return Success(response);
    }

    public ApiResultResponse<UserDetailResponse> GetAsync(
        [FromServices] IEventBus eventBus,
        [FromQuery] Guid id)
    {
        var response = new UserDetailResponse();
        return Success(response);
    }

    public ApiResultResponseBase CreateAsync(
        [FromServices] IEventBus eventBus,
        [FromBody] Guid id)
    {
        return Success();
    }

    public ApiResultResponseBase DeleteAsync(
        [FromServices] IEventBus eventBus,
        [FromBody] Guid id)
    {
        eventBus.PublishAsync(new DeleteCommand { Id = id });
        return Success();
    }
}
