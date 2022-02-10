namespace MASA.Framework.Admin.Service.Users.Services;

public class UserServices : ServiceBase
{
    public UserServices(IServiceCollection services) : base(services)
    {
        App.MapGet("/api/users/items", () => GetItemsAsync);
        App.MapGet("/api/users/{id}", () => GetAsync);
    }

    private PaginatedItemsViewModel<UserItemsResponse> GetItemsAsync(
        [FromServices] IEventBus eventBus,
        [FromQuery] UserType type,
        [FromQuery] int pageIndex = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] string account = "",
        [FromQuery] int state = -1)
    {
        return new PaginatedItemsViewModel<UserItemsResponse>(pageIndex, pageSize, 0, new List<UserItemsResponse>());
    }

    private UserDetailResponse GetAsync(
        [FromServices] IEventBus eventBus,
        [FromQuery] Guid guid)
    {
        return new UserDetailResponse();
    }
}
