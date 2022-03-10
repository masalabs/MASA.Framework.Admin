using Masa.Framework.Admin.Service.User.Application.Organizations.Commands;
using CreateCommand = Masa.Framework.Admin.Service.User.Application.Organizations.Commands.CreateCommand;

namespace Masa.Framework.Admin.Service.User.Services;

public class DepartmentService : ServiceBase
{
    public DepartmentService(IServiceCollection services) : base(services)
    {
        App.MapGet(Routing.DepartmentList, GetTreeListAsync);
        App.MapPost(Routing.OperateDepartment, CreateAsync);
        App.MapPost(Routing.DepartmentUsers, UpdateDepartmentUsersAsync);
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

    public async Task UpdateDepartmentUsersAsync(
        [FromServices] IEventBus eventBus,
        [FromBody] UpdateDepartmentUserRequest updateDepartmentUserRequest)
    {
        var command = new UpdateDepartmentUserCommand(updateDepartmentUserRequest);
        await eventBus.PublishAsync(command);
    }
}

