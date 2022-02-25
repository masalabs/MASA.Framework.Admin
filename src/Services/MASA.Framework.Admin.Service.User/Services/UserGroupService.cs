using CreateCommand = MASA.Framework.Admin.Service.User.Application.UserGroups.Commands.CreateCommand;
using ListQuery = MASA.Framework.Admin.Service.User.Application.UserGroups.Queres.ListQuery;

namespace MASA.Framework.Admin.Service.User.Services
{
    public class UserGroupService : ServiceBase
    {
        public UserGroupService(IServiceCollection services) : base(services)
        {
            App.MapGet(Routing.UserGroupList, GetItemsAsync);
            App.MapGet(Routing.UserGroupSelect, GetSelectListAsync);
            App.MapGet(Routing.GroupByUser, GetUserGroupListAsync);
            App.MapPost(Routing.OperateGroup, CreateAsync);
            App.MapDelete(Routing.OperateGroup, DeleteAsync);
        }

        public async Task<PaginatedItemResponse<UserGroupItemResponse>> GetItemsAsync(
            [FromServices] IEventBus eventBus,
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 20,
            [FromQuery] string name = "")
        {
            var listQuery = new ListQuery(pageIndex, pageSize, name);
            await eventBus.PublishAsync(listQuery);
            return listQuery.Result;
        }

        public async Task CreateAsync(
            [FromServices] IEventBus eventBus,
            [FromHeader(Name = "creator-id")] Guid creator,
            [FromBody] CreateUserGroupRequest createUserGroupRequest)
        {
            await eventBus.PublishAsync(new CreateCommand(createUserGroupRequest)
            {
                Creator = creator
            });
        }

        public async Task<List<UserGroupItemResponse>> GetSelectListAsync(
            [FromServices] IEventBus eventBus,
            [FromQuery] Guid userId)
        {
            var query = new SelectQuery(userId);
            await eventBus.PublishAsync(query);
            return query.Result;
        }

        public async Task<List<UserGroupItemResponse>> GetUserGroupListAsync(
            [FromServices] IEventBus eventBus,
            [FromQuery] Guid userId)
        {
            var query = new UserGroupQuery(userId);
            await eventBus.PublishAsync(query);
            return query.Result;
        }

        public async Task DeleteAsync(
            [FromServices] IEventBus eventBus, Guid id)
        {
            await eventBus.PublishAsync(new DeleteCommand(id));
        }

    }
}
