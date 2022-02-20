namespace MASA.Framework.Admin.Service.Authentication.Application.Objects;

public class QueryHandler
{
    private readonly IRepository<Domain.Aggregates.ObjectAggregate.Object> _repository;

    public QueryHandler(IRepository<Domain.Aggregates.ObjectAggregate.Object> repository)
    {
        _repository = repository;
    }

    [EventHandler]
    public async Task GetListAsync(ListObjectQuery query)
    {
        Expression<Func<Domain.Aggregates.ObjectAggregate.Object, bool>> condition = obj => true;
        if (!string.IsNullOrEmpty(query.Name))
            condition = condition.And(obj => obj.Name.Contains(query.Name) || obj.Code.Contains(query.Name));

        if (query.Type != -1)
            condition = condition.And(obj => obj.ObjectType == (ObjectType)query.Type);

        var objectItems = await _repository.GetPaginatedListAsync(condition, new PaginatedOptions()
        {
            Page = query.PageIndex,
            PageSize = query.PageSize,
            Sorting = new Dictionary<string, bool>
            {
                [nameof(Domain.Aggregates.ObjectAggregate.Object.ModificationTime)] = true,
                [nameof(Domain.Aggregates.ObjectAggregate.Object.CreationTime)] = true,
            }
        });
        query.Result = new PaginatedItemResponse<ObjectItemResponse>(
            query.PageIndex,
            query.PageSize,
            objectItems.TotalPages,
            objectItems.Total,
            objectItems.Result.Select(obj => new ObjectItemResponse()
            {
                Id = obj.Id,
                Code = obj.Code,
                Name = obj.Name,
                Enable = obj.Enable,
                ObjectType = obj.ObjectType
            }));
    }

    [EventHandler]
    public async Task GetListAsync(AllObjectQuery query)
    {
        var objectItems = await _repository.GetListAsync();
        query.Result = objectItems.Select(obj => new ObjectItemResponse()
        {
            Id = obj.Id,
            Code = obj.Code,
            Name = obj.Name,
            Enable = obj.Enable,
            ObjectType = obj.ObjectType
        }).ToList();
    }

    [EventHandler]
    public async Task ContainsAsync(ContainsObjectQuery query)
    {
        var contains = await _repository.GetCountAsync(o => o.Id != query.ObjectId && o.Code == query.ObjectCode);
        query.Result = contains > 0;
    }
}
