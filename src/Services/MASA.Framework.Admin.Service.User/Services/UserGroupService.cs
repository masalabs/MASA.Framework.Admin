namespace MASA.Framework.Admin.Service.User.Services
{
    public class UserGroupService : ServiceBase
    {
        public UserGroupService(IServiceCollection services) : base(services)
        {
        }

        //public async Task<PaginatedItemResponse<UserGroupItemResponse>> GetItemsAsync(
        //    [FromServices] IEventBus eventBus,
        //    [FromQuery] int pageIndex = 1,
        //    [FromQuery] int pageSize = 20,
        //    [FromQuery] string account = "")
        //{

        //    var listQuery = new ListQuery(pageIndex, pageSize, account);
        //    await eventBus.PublishAsync(listQuery);

        //    return listQuery.Result;
        //}

        //public async Task CreateAsync(
        //    [FromServices] IEventBus eventBus,
        //    [FromHeader(Name = "creator-id")] Guid creator,
        //    [FromBody] CreateUserRequest userCreateRequest)
        //{
        //    await eventBus.PublishAsync(new CreateCommand(userCreateRequest)
        //    {
        //        Creator = creator
        //    });
        //}

        //public async Task DeleteAsync(
        //    [FromServices] IEventBus eventBus, Guid id)
        //{
        //    await eventBus.PublishAsync(new DeleteCommand(id));
        //}

    }
}
