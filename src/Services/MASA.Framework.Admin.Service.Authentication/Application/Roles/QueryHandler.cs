namespace MASA.Framework.Admin.Service.Authentication.Application.Roles;

public class QueryHandler
{
    private readonly IRoleRepository _repository;

    public QueryHandler(IRoleRepository repository)
    {
        _repository = repository;
    }

    [EventHandler]
    public async Task GetListAsync(RoleQuery.ListQuery query)
    {
        Expression<Func<Role, bool>> condition = rule => true;
        if (!string.IsNullOrEmpty(query.Name))
            condition = condition.And(role => role.Name.Contains(query.Name));

        if (query.State != -1)
            condition = condition.And(role => role.State == (State)query.State);

        var roles = await _repository.GetPaginatedListAsync(
            condition,
            new PaginatedOptions()
            {
                Page = query.PageIndex,
                PageSize = query.PageSize
            });

        query.Result = new PaginatedItemResponse<RoleItemResponse>(query, roles.Total, roles.TotalPages, roles.Result.Select(role
            => new RoleItemResponse()
            {
                Id = role.Id,
                Name = role.Name,
                Describe = role.Describe,
                Number = role.Number,
                State = role.State,
                CreationTime = role.CreationTime
            }));
    }

    [EventHandler]
    public async Task GetDetailAsync(RoleQuery.DetailQuery query)
    {
        var role = await _repository.FindAsync(query.RoleId);
        if (role == null)
            throw new UserFriendlyException("the role does not exist");

        var childrenRoleIds = role.RoleItems.Select(role => role.ChildrenRoleId).ToList();
        var childrenRoles = await _repository.GetListAsync(role => childrenRoleIds.Contains(role.Id));

        query.Result = new RoleDetailResponse()
        {
            Id = role.Id,
            Name = role.Name,
            Describe = role.Describe,
            Number = role.Number,
            State = role.State,
            CreationTime = role.CreationTime,
            ChildrenRoles = role.RoleItems.Select(item =>
                new KeyValuePair<Guid, string>(
                    item.ChildrenRoleId,
                    childrenRoles.Where(childrenRole => childrenRole.Id == item.ChildrenRoleId)
                        .Select(childrenRole => childrenRole.Name).FirstOrDefault() ?? string.Empty)).ToList()
        };
    }
}
