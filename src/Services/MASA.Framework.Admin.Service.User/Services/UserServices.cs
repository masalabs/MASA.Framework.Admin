using MASA.Framework.Admin.Contracts.Login.Model;
using Microsoft.Extensions.Caching.Memory;

namespace MASA.Framework.Admin.Service.User.Services;

public class UserServices : ServiceBase
{
    private static ActivitySource activitySource = new ActivitySource(TelemetryConstants.ServiceName);

    public UserServices(IServiceCollection services) : base(services)
    {
        App.MapGet(Routing.UserList, GetItemsAsync);
        App.MapGet(Routing.UserDetail, GetAsync);
        App.MapGet(Routing.UserRole, GetUserRolesAsync);
        App.MapPost(Routing.OperateUser, CreateAsync);
        App.MapPost(Routing.UserRole, CreateUserRoleAsync);
        App.MapDelete(Routing.OperateUser, DeleteAsync); ;
    }

    public async Task<PaginatedItemResponse<UserItemResponse>> GetItemsAsync(
        [FromServices] IEventBus eventBus,
        [FromQuery] int pageIndex = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] string account = "",
        [FromQuery] int state = -1)
    {
        using var activity = activitySource.StartActivity("GetItemsAsync", ActivityKind.Internal);

        var listQuery = new ListQuery(pageIndex, pageSize, account);
        await eventBus.PublishAsync(listQuery);

        activity?.AddEvent(new ActivityEvent("Load Users",
            tags: new ActivityTagsCollection(new[] {
                KeyValuePair.Create<string, object?>("Count", listQuery.Result.Items.Count())
            })));
        activity?.SetTag("user_count", listQuery.Result.Items.Count());
        activity?.SetTag("status_code", "OK");
        activity?.SetTag("status_description", "Load successfully");

        return listQuery.Result;
    }

    public async Task<List<UserRoleResponse>> GetUserRolesAsync(
        [FromServices] IEventBus eventBus,
        [FromQuery] Guid userId)
    {
        var rolesQuery = new RoleListQuery(userId);
        await eventBus.PublishAsync(rolesQuery);
        return rolesQuery.Result;
    }

    public async Task<UserDetailResponse> GetAsync(
        [FromServices] IEventBus eventBus, Guid id)
    {
        var detailQuery = new DetailQuery(id);
        await eventBus.PublishAsync(detailQuery);
        return detailQuery.Result;
    }

    public async Task CreateAsync(
        [FromServices] IEventBus eventBus,
        [FromHeader(Name = "creator-id")] Guid creator,
        [FromBody] CreateUserRequest userCreateRequest)
    {
        await eventBus.PublishAsync(new CreateCommand(userCreateRequest)
        {
            Creator = creator
        });
    }

    public async Task CreateUserRoleAsync(
        [FromServices] IEventBus eventBus,
        [FromHeader(Name = "creator-id")] Guid creator,
        [FromBody] CreateUserRoleRequest userRoleCreateRequest)
    {
        await eventBus.PublishAsync(new CreateRoleCommand(userRoleCreateRequest)
        {
            Creator = creator
        });
    }

    public async Task DeleteAsync(
        [FromServices] IEventBus eventBus, Guid id)
    {
        await eventBus.PublishAsync(new DeleteCommand(id));
    }

    [HttpPost]
    public async Task<LoginViewModel> Login([FromServices] IUserRepository userRepository, LoginModel model)
    {
        var result = await userRepository.LoginAsync(model);

        return result;
    }

    [HttpGet]
    public async Task<UserModel> GetUser([FromServices] IUserRepository userRepository, [FromBody] int id)
    {
        var result = await userRepository.GetUserAsync(id);

        return result;
    }

    public async Task GetOnlineUserCount(
       [FromServices] IEventBus eventBus, [FromServices] IMemoryCache _memoryCache,
       [FromHeader(Name = "creator-id")] Guid creator)
    {
        await eventBus.PublishAsync(new CreateRoleCommand(userRoleCreateRequest)
        {
            Creator = creator
        });

        //var users = _memoryCache.Get<List<OnlineUserModel>>("online_user_id");
        //return users?.Count ?? 0;
    }

    [HttpGet]
    public async Task<int> GetUserCount([FromServices] IUserRepository userRepository)
    {
        var count = await userRepository.GetUserCount();

        return count;
    }

}
