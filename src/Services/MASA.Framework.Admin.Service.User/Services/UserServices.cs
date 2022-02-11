namespace MASA.Framework.Admin.Service.User.Services;

public class UserServices : CustomServiceBase
{
    public UserServices(IServiceCollection services) : base(services)
    {
        App.MapGet(Routing.UserList, () => GetItemsAsync);
        App.MapGet(Routing.UserDetail, () => GetAsync);
        App.MapPost(Routing.UserCreate, () => CreateAsync);
        App.MapDelete(Routing.UserDelete, () => DeleteAsync);
    }

    private ApiResultResponse<PaginatedItemsViewModel<UserItemResponse>> GetItemsAsync(
        [FromServices] IEventBus eventBus,
        [FromQuery] UserType type,
        [FromQuery] int pageIndex = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] string account = "",
        [FromQuery] int state = -1)
    {
        var response = new PaginatedItemsViewModel<UserItemResponse>(pageIndex, pageSize, 0, new List<UserItemResponse>());
        return Success(response);
    }

    private ApiResultResponse<UserDetailResponse> GetAsync(
        [FromServices] IEventBus eventBus,
        [FromQuery] Guid guid)
    {
        var response = new UserDetailResponse();
        return Success(response);
    }

    private ApiResultResponseBase CreateAsync(
        [FromServices] IEventBus eventBus,
        [FromBody] Guid guid)
    {
        return Success();
    }

    private ApiResultResponseBase DeleteAsync(
        [FromServices] IEventBus eventBus,
        [FromBody] Guid guid)
    {
        return Success();
    }
}
