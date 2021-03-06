using IdListQuery = Masa.Framework.Admin.Service.Authentication.Application.Permissions.Queries.IdListQuery;

namespace Masa.Framework.Admin.Service.Authentication.Application.Permissions;

public class PermissionsQueryHandler
{
    private readonly IRepository<Permission, Guid> _repository;

    public PermissionsQueryHandler(IRepository<Permission, Guid> repository)
    {
        _repository = repository;
    }

    [EventHandler]
    public async Task GetListAsync(PermissionListQuery query)
    {
        Expression<Func<Permission, bool>> condition = permission => true;
        if (query.Enabled)
            condition = condition.And(permission => permission.Enable == true);
        else
            condition = condition.And(permission => permission.Enable == false);

        if (!string.IsNullOrEmpty(query.Name))
            condition = condition.And(permission => permission.Name.Contains(query.Name));

        var permissions = await _repository.GetPaginatedListAsync(condition, new PaginatedOptions()
        {
            Page = query.PageIndex,
            PageSize = query.PageSize
        });

        query.Result = new PaginatedItemResponse<PermissionItemResponse>(
            query.PageIndex,
            query.PageSize,
            permissions.Total,
            permissions.TotalPages,
            permissions.Result.Select(permission => new PermissionItemResponse()
            {
                Id = permission.Id,
                ObjectType = permission.ObjectType,
                Name = permission.Name,
                Resource = permission.Resource,
                Scope = permission.Scope,
                Action = permission.Action,
                Enable = permission.Enable,
                PermissionType = permission.PermissionType,
                ModificationTime = permission.ModificationTime,
            }).ToList());
    }

    [EventHandler]
    public async Task GetDetailAsync(PermissionDetailQuery query)
    {
        var permission = await _repository.FindAsync(query.PermissionId);
        if (permission == null)
            throw new UserFriendlyException("permission information does not exist");

        query.Result = new PermissionDetailResponse()
        {
            Id = permission.Id,
            ObjectType = permission.ObjectType,
            Name = permission.Name,
            Resource = permission.Resource,
            Scope = permission.Scope,
            Action = permission.Action,
            PermissionType = permission.PermissionType,
        };
    }

    [EventHandler]
    public async Task GetPermissionListByIdQuery(IdListQuery query)
    {
        query.Result = (await _repository.GetListAsync((r) => query.IdList.Contains(r.Id)))
            .Select(role => new PermissionItemResponse
            {
                Id = role.Id,
                Name = role.Name,
                ObjectType = role.ObjectType,
                Action = role.Action,
                Scope = role.Scope,
                Resource = role.Resource,
                Enable = role.Enable,
            }).ToList();
    }
}
