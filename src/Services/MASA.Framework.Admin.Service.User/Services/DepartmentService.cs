using CreateCommand = MASA.Framework.Admin.Service.User.Application.Organizations.Commands.CreateCommand;

namespace MASA.Framework.Admin.Service.User.Services;

public class DepartmentService : ServiceBase
{
    public DepartmentService(IServiceCollection services) : base(services)
    {
        App.MapGet(Routing.DepartmentList, GetTreeListAsync);
        App.MapGet(Routing.DepartmentUsers, GetDepartmentUsersAsync);
        App.MapPost(Routing.OperateDepartment, CreateAsync);
    }

    public async Task CreateAsync(
        [FromServices] IEventBus eventBus,
        [FromHeader(Name = "creator-id")] Guid creator,
        [FromBody] CreateDepartmentRequest createDepartmentRequest)
    {
        await eventBus.PublishAsync(new CreateCommand(createDepartmentRequest)
        {
            Creator = creator
        });
    }

    public async Task<List<DepartmentItemResponse>> GetTreeListAsync(
        [FromServices] IEventBus eventBus,
        [FromQuery] Guid parentId)
    {
        var query = new TreeQuery(parentId);
        await eventBus.PublishAsync(query);
        return query.Result;
    }

    public async Task<PaginatedItemResponse<UserItemResponse>> GetDepartmentUsersAsync(
        [FromServices] IEventBus eventBus,
        [FromQuery] Guid departmentId,
        [FromQuery] int pageIndex = 1,
        [FromQuery] int pageSize = 20
        )
    {
        var query = new DepartmentUserQuery(pageIndex, pageSize,departmentId);
        await eventBus.PublishAsync(query);
        return query.Result;
    }
}

