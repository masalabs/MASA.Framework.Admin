namespace MASA.Framework.Admin.Service.Authentication.Services;

public class ObjectService : ServiceBase
{
    public ObjectService(IServiceCollection services) : base(services)
    {
        App.MapGet(Routing.ObjectList, GetItemsAsync);
        App.MapGet(Routing.ObjectAll, GetAllAsync);
        App.MapGet(Routing.ContainsObject, ContainsAsync);
        App.MapPost(Routing.OperateObject, AddAsync);
        App.MapPut(Routing.OperateObject, EditAsync);
        App.MapDelete(Routing.OperateObject, DeleteAsync);
        App.MapDelete(Routing.BatchDeleteObject, BatchDeleteAsync);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="eventBus"></param>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize">When the page number is -1, no pagination is performed at this time，default: 20</param>
    /// <param name="type"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public async Task<PaginatedItemResponse<ObjectItemResponse>> GetItemsAsync(
        [FromServices] IEventBus eventBus,
        [FromQuery] int pageIndex = QueryConfig.DEFAULT_PAGE_INDEX,
        [FromQuery] int pageSize = QueryConfig.DEFAULT_PAGE_SIZE,
        [FromQuery] int type = -1,
        [FromQuery] string name = "")
    {
        var query = new ListObjectQuery(pageIndex, pageSize, type, name);
        await eventBus.PublishAsync(query);
        return query.Result;
    }

    public async Task<List<ObjectItemResponse>> GetAllAsync([FromServices] IEventBus eventBus)
    {
        var query = new AllObjectQuery();
        await eventBus.PublishAsync(query);
        return query.Result;
    }

    public async Task<bool> ContainsAsync(
        [FromServices] IEventBus eventBus,
        [FromQuery] Guid objectId,
        [FromQuery] string code)
    {
        var query = new ContainsObjectQuery(objectId, code);
        await eventBus.PublishAsync(query);
        return query.Result;
    }

    public async Task AddAsync(
        [FromServices] IEventBus eventBus,
        [FromBody] AddObjectCommand command)
    {
        await eventBus.PublishAsync(command);
    }

    public async Task EditAsync(
        [FromServices] IEventBus eventBus,
        [FromBody] EditObjectCommand command)
    {
        await eventBus.PublishAsync(command);
    }

    public async Task DeleteAsync(
        [FromServices] IEventBus eventBus,
        [FromBody] DeleteObjectCommand command)
    {
        await eventBus.PublishAsync(command);
    }

    public async Task BatchDeleteAsync(
        [FromServices] IEventBus eventBus,
        [FromBody] BatchDeleteObjectCommand command)
    {
        await eventBus.PublishAsync(command);
    }
}
