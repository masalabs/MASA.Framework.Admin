namespace MASA.Framework.Admin.Service.Authentication.Application.Objects;

public class QueryHandler
{
    private readonly IObjectRepository _repository;

    public QueryHandler(IObjectRepository repository)
    {
        _repository = repository;
    }

    [EventHandler]
    public async Task GetListAsync(ObjectQueries.ListQuery query)
    {
        Expression<Func<Domain.Aggregate.ObjectAggregate.Object, bool>> condition = obj => true;
        if (!string.IsNullOrEmpty(query.Name))
            condition = condition.And(obj => obj.Name.Contains(query.Name));

        if (query.Type != -1)
            condition = condition.And(obj => obj.ObjectType == (ObjectType)query.Type);

        var objectItems = await _repository.GetPaginatedListAsync(condition, new PaginatedOptions()
        {
            Page = query.PageIndex,
            PageSize = query.PageSize
        });
        query.Result = new PaginatedItemResponse<ObjectItemResponse>(query, objectItems.Total, objectItems.TotalPages,
            objectItems.Result.Select(obj => new ObjectItemResponse()
            {
                Id = obj.Id,
                Code = obj.Code,
                Name = obj.Name,
                State = obj.State,
                ObjectType = obj.ObjectType
            }));
    }
}
