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
            condition = condition.And(obj => obj.Name.Contains(query.Name) || obj.Code.Contains(query.Name));

        if (query.Type != -1)
            condition = condition.And(obj => obj.ObjectType == (ObjectType)query.Type);

        var objectItems = await _repository.GetPaginatedListAsync(condition, new PaginatedOptions()
        {
            Page = query.PageIndex,
            PageSize = query.PageSize,
            Sorting = new Dictionary<string, bool>
            {
                [nameof(Domain.Aggregate.ObjectAggregate.Object.ModificationTime)] = true,
                [nameof(Domain.Aggregate.ObjectAggregate.Object.CreationTime)] = true,
            }
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

    [EventHandler(Order = 2)]
    public async Task GetDetailAsync(RoleQuery.DetailQuery query)
    {
        var permissionIds = query.Result.Permissions.Select(permission => permission.PermissionId).ToList();
        var objectItems = (await _repository.GetListAsync(obj => obj.Permissions.Any(permission => permissionIds.Contains(permission.Id))))
            .ToList();
        foreach (var permission in query.Result.Permissions)
        {
            var obj = objectItems.FirstOrDefault(obj
                => obj.Permissions.Any(item => item.Id == permission.PermissionId))!; //The permission policy does not allow deletion
            var permissionInfo = obj.Permissions.FirstOrDefault(item => item.Id == permission.PermissionId)!;
            permission.PermissionName = permissionInfo.Name;
            permission.ObjectType = obj.ObjectType;
            permission.ObjectCode = obj.Code;
            permission.ObjectIdentifies = permission.ObjectIdentifies;
        }
    }

    [EventHandler(Order = 3)]
    public async Task ContainsAsync(ObjectQueries.ContainsQuery query)
    {
        var contains = await _repository.GetCountAsync(o => o.Id != query.ObjectId && o.Code == query.ObjectCode);
        query.Result = contains > 0;
    }
}
