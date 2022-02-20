using MASA.Framework.Admin.Contracts.User.Const;
using System.Diagnostics;

namespace MASA.Framework.Admin.Service.User.Services;

public class UserServices : ServiceBase
{
    private ActivitySource activitySource;

    public UserServices(IServiceCollection services) : base(services)
    {
        App.MapGet(Routing.UserList, GetItemsAsync);
        App.MapGet(Routing.UserDetail, GetAsync);
        App.MapGet(Routing.UserRole, GetUserRolesAsync);
        App.MapPost(Routing.OperateUser, CreateAsync);
        App.MapPost(Routing.UserRole, CreateUserRoleAsync);
        App.MapDelete(Routing.OperateUser, DeleteAsync);

        activitySource = new ActivitySource(TelemetryConstants.ServiceName);
    }

    public async Task<PaginatedItemResponse<UserItemResponse>> GetItemsAsync(
        [FromServices] IEventBus eventBus,
        [FromQuery] int pageIndex = Config.DEFAULT_PAGE_INDEX,
        [FromQuery] int pageSize = Config.DEFAULT_PAGE_SIZE,
        [FromQuery] string account = "",
        [FromQuery] int state = -1)
    {
        using var activity = activitySource.StartActivity(TelemetryConstants.ServiceName, ActivityKind.Internal)!;

        var listQuery = new UserQuery.ListQuery(pageIndex, pageSize, account);
        await eventBus.PublishAsync(listQuery);

        activity.AddEvent(new ActivityEvent("Load Users",
            tags: new ActivityTagsCollection(new[] {
                KeyValuePair.Create<string, object?>("Count", listQuery.Result.Items.Count())
            })));
        activity.SetTag("user_count", listQuery.Result.Items.Count());
        activity.SetTag("status_code", "OK");
        activity.SetTag("status_description", "Load successfully");

        return listQuery.Result;
    }

    public async Task<List<UserRoleResponse>> GetUserRolesAsync(
        [FromServices] IEventBus eventBus,
        [FromQuery] Guid userId)
    {
        var rolesQuery = new UserQuery.RoleListQuery(userId);
        await eventBus.PublishAsync(rolesQuery);
        return rolesQuery.Result;
    }

    public async Task<UserDetailResponse> GetAsync(
        [FromServices] IEventBus eventBus, Guid id)
    {
        var detailQuery = new UserQuery.DetailQuery(id);
        await eventBus.PublishAsync(detailQuery);
        return detailQuery.Result;
    }

    public async Task CreateAsync(
        [FromServices] IEventBus eventBus,
        [FromHeader(Name = "creator-id")] Guid creator,
        [FromBody] CreateUserRequest userCreateRequest)
    {
        await eventBus.PublishAsync(new UserCommand.CreateCommand(userCreateRequest)
        {
            Creator = creator
        });
    }

    public async Task CreateUserRoleAsync(
        [FromServices] IEventBus eventBus,
        [FromHeader(Name = "creator-id")] Guid creator,
        [FromBody] CreateUserRoleRequest userRoleCreateRequest)
    {
        await eventBus.PublishAsync(new UserCommand.CreateRoleCommand(userRoleCreateRequest)
        {
            Creator = creator
        });
    }

    public async Task DeleteAsync(
        [FromServices] IEventBus eventBus, Guid id)
    {
        await eventBus.PublishAsync(new UserCommand.DeleteCommand(id));
    }
}
