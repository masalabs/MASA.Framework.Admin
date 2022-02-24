using CreateCommand = MASA.Framework.Admin.Service.User.Application.Users.Commands.CreateCommand;
using ListQuery = MASA.Framework.Admin.Service.User.Application.Users.Queres.ListQuery;

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
        App.MapDelete(Routing.OperateUser, DeleteAsync);
        App.MapPost(Routing.UserLogin, LoginAsync);
        App.MapGet(Routing.UserStatistic, GetUserCountAsync);
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
    public async Task<string> LoginAsync([FromServices] IEventBus eventBus, [FromBody] UserLoginRequest userLoginRequest)
    {
        var loginCommand = new LoginCommand(userLoginRequest);
        await eventBus.PublishAsync(loginCommand);

        return loginCommand.Token;
    }

    public async Task<UserStatisticResponse> GetUserCountAsync(
       [FromServices] IEventBus eventBus)
    {
        var statisticQuery = new StatisticQuery();
        await eventBus.PublishAsync(statisticQuery);

        return statisticQuery.Result;
    }

}
