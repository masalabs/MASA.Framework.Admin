namespace MASA.Framework.Admin.Service.Authentication.Services;

public class ObjectService : CustomServiceBase
{
    public ObjectService(IServiceCollection services) : base(services)
    {
        App.MapGet(Routing.ObjectList, GetItemsAsync);
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
    public async Task<ApiResultResponse<PaginatedItemResponse<ObjectItemResponse>>> GetItemsAsync(
        [FromServices] IEventBus eventBus,
        [FromQuery] int pageIndex = Config.DEFAULT_PAGE_INDEX,
        [FromQuery] int pageSize = Config.DEFAULT_PAGE_SIZE,
        [FromQuery] int type = -1,
        [FromQuery] string name = "")
    {
        var query = new ObjectQueries.ListQuery(pageIndex, pageSize, type, name);
        await eventBus.PublishAsync(query);
        return Success(query.Result);
    }

    public async Task<ApiResultResponse<bool>> ContainsAsync(
        [FromServices] IEventBus eventBus,
        [FromQuery] Guid objectId,
        [FromQuery] string code)
    {
        var query = new ObjectQueries.ContainsQuery(objectId,code);
        await eventBus.PublishAsync(query);
        return Success(query.Result);
    }

    public async Task<ApiResultResponseBase> AddAsync(
         [FromServices] IEventBus eventBus,
         [FromHeader(Name = "creator-id")] Guid creatorId,
         [FromBody] AddObjectRequest request)
    {
        await eventBus.PublishAsync(new ObjectCommand.AddCommand(request)
        {
            Creator = creatorId
        });
        return Success();
    }

    public async Task<ApiResultResponseBase> EditAsync(
        [FromServices] IEventBus eventBus,
        [FromHeader(Name = "creator-id")] Guid creatorId,
        [FromBody] EditObjectRequest request)
    {
        await eventBus.PublishAsync(new ObjectCommand.EditCommand(request)
        {
            Creator = creatorId
        });
        return Success();
    }

    public async Task<ApiResultResponseBase> DeleteAsync(
    [FromServices] IEventBus eventBus,
    [FromHeader(Name = "creator-id")] Guid creatorId,
    [FromBody] DeleteObjectRequest request)
    {
        await eventBus.PublishAsync(new ObjectCommand.DeleteCommand(request)
        {
            Creator = creatorId
        });
        return Success();
    }

    public async Task<ApiResultResponseBase> BatchDeleteAsync(
    [FromServices] IEventBus eventBus,
    [FromHeader(Name = "creator-id")] Guid creatorId,
    [FromBody] BatchDeleteObjectRequest request)
    {
        await eventBus.PublishAsync(new ObjectCommand.BatchDeleteCommand(request)
        {
            Creator = creatorId
        });
        return Success();
    }
}
