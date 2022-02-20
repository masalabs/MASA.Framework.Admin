namespace MASA.Framework.Admin.Service.Authentication.Application.Roles;

public class RoleQueryHandler
{
    private readonly IRoleRepository _repository;

    public RoleQueryHandler(IRoleRepository repository)
    {
        _repository = repository;
    }

    [EventHandler]
    public async Task GetListAsync(RoleQuery.RoleListQuery query)
    {
        Expression<Func<Role, bool>> condition = rule => true;
        if (!string.IsNullOrEmpty(query.Name))
            condition = condition.And(role => role.Name.Contains(query.Name));

        if (query.State == 1)
            condition = condition.And(role => role.Enable == true);
        else if (query.State == 0)
            condition = condition.And(role => role.Enable == false);

        var roles = await _repository.GetPaginatedListAsync(
            condition,
            new PaginatedOptions()
            {
                Page = query.PageIndex,
                PageSize = query.PageSize
            });

        query.Result = new PaginatedItemResponse<RoleItemResponse>(query.PageIndex, query.PageSize, roles.Total, roles.TotalPages,
            roles.Result.Select(role
                => new RoleItemResponse()
                {
                    Id = role.Id,
                    Name = role.Name,
                    Describe = role.Describe,
                    Number = role.Number,
                    Enable = role.Enable,
                    CreationTime = role.CreationTime
                }));
    }

    [EventHandler(Order = 1)]
    public async Task GetDetailAsync(RoleQuery.RoleDetailQuery query)
    {
        var role = await GetPermissionListByLoopAsync(query.RoleId);

        List<AuthorizeItemResponse> permissions = role.Permissions.Select(permission => new AuthorizeItemResponse()
        {
            Id = permission.Id,
            InheritanceRoleSource = permission.RoleName,
            PermissionId = permission.PermissionId,
            PermissionEffect = permission.PermissionEffect,
            PermissionType = permission.PermissionType,
        }).ToList();
        query.Result = new RoleDetailResponse
        {
            Id = role.Id,
            Name = role.Name,
            Describe = role.Describe,
            Number = role.Number,
            Enable = role.Enable,
            CreationTime = role.CreationTime,
            ChildrenRoles = role.ChildrenRoles.Select(childrenGuid => new KeyValuePair<Guid, string>(childrenGuid,
                role.Permissions.Where(permission => permission.RoleId == childrenGuid).Select(permission => permission.RoleName)
                    .FirstOrDefault() ??
                string.Empty)).ToList(),
            Permissions = permissions
        };

    }

    private async Task<RoleDetailDto> GetPermissionListByLoopAsync(Guid roleId)
    {
        var role = await GetDetailAsync(roleId);
        if (role.ChildrenRoles.Any())
        {
            List<List<AuthorizeItemDto>> childrenRolePermissions = new();
            foreach (var childrenRoleId in role.ChildrenRoles)
            {
                var childrenRole = await GetPermissionListByLoopAsync(childrenRoleId);
                childrenRolePermissions.Add(childrenRole.Permissions);
            }
            var inheritPermissions =
                await GetPermissionListAsync(role.Permissions, await GetInheritPermissionListAsync(childrenRolePermissions));
            role.Permissions = inheritPermissions;
        }
        return role;
    }

    /// <summary>
    /// Get the last permission based on the current permission and the inherited permission set
    /// </summary>
    /// <param name="currentRolePermissions"></param>
    /// <param name="inheritedPermissions"></param>
    /// <returns></returns>
    private Task<List<AuthorizeItemDto>> GetPermissionListAsync(
        List<AuthorizeItemDto> currentRolePermissions,
        List<AuthorizeItemDto> inheritedPermissions)
    {
        foreach (var inheritedPermission in inheritedPermissions)
        {
            if (currentRolePermissions.All(permission => permission.PermissionId != inheritedPermission.PermissionId))
            {
                currentRolePermissions.Add(inheritedPermission);
            }
        }
        return Task.FromResult(inheritedPermissions);
    }

    /// <summary>
    /// get inherited permissions
    /// </summary>
    /// <param name="childrenPermissions"></param>
    /// <returns></returns>
    /// <exception cref="UserFriendlyException"></exception>
    private Task<List<AuthorizeItemDto>> GetInheritPermissionListAsync(List<List<AuthorizeItemDto>> childrenPermissions)
    {
        List<AuthorizeItemDto> permissions = new();
        foreach (var authorizeItems in childrenPermissions)
        {
            foreach (var authorizeItem in authorizeItems)
            {
                if (authorizeItem.PermissionType == PermissionType.Public)
                {
                    var item = permissions.FirstOrDefault(permission => permission.PermissionId == authorizeItem.PermissionId);
                    if (item != null)
                    {
                        if (item.PermissionEffect == PermissionEffect.Deny || authorizeItem.PermissionEffect == PermissionEffect.Deny)
                        {
                            item.PermissionEffect = PermissionEffect.Deny;
                        }
                    }
                    else
                    {
                        permissions.Add(new AuthorizeItemDto()
                        {
                            Id = authorizeItem.Id,
                            RoleId = authorizeItem.RoleId,
                            RoleName = authorizeItem.RoleName,
                            PermissionId = authorizeItem.PermissionId,
                            PermissionEffect = authorizeItem.PermissionEffect,
                            PermissionType = authorizeItem.PermissionType
                        });
                    }
                }
                else if (authorizeItem.PermissionType == PermissionType.Private)
                {
                    //Private permissions are not inherited
                }
                else
                    throw new UserFriendlyException("Unsupported license type");
            }
        }
        return Task.FromResult(permissions);
    }

    private async Task<RoleDetailDto> GetDetailAsync(Guid roleId)
    {
        var role = await _repository.FindAsync(roleId);
        if (role == null)
            throw new UserFriendlyException("the role does not exist");

        var list = new RoleDetailDto()
        {
            Id = role.Id,
            Name = role.Name,
            Describe = role.Describe,
            Number = role.Number,
            Enable = role.Enable,
            CreationTime = role.CreationTime,
            ChildrenRoles = role.RoleItems.Select(roleItem => roleItem.ParentRoleId).ToList(),
            Permissions = role.Permissions.Select(permission => new AuthorizeItemDto()
            {
                Id = permission.Id,
                RoleId = permission.Role.Id,
                RoleName = permission.Role.Name,
                PermissionId = permission.PermissionsId,
                PermissionType = permission.PermissionType,
                PermissionEffect = permission.PermissionEffect
            }).ToList()
        };
        return list;
    }

    private class RoleDetailDto : RoleItemResponse
    {
        public List<Guid> ChildrenRoles { get; set; } = new();

        public List<AuthorizeItemDto> Permissions { get; set; } = new();
    }

    private class AuthorizeItemDto
    {
        /// <summary>
        /// Primary key id of Role and Permission relationship table
        /// </summary>
        public Guid Id { get; set; }

        public Guid RoleId { get; set; } = default!;

        /// <summary>
        /// The name of the role to which the license was granted
        /// </summary>
        public string RoleName { get; set; } = default!;

        public Guid PermissionId { get; set; }

        public PermissionType PermissionType { get; set; }

        public PermissionEffect PermissionEffect { get; set; }
    }

    [EventHandler]
    public async Task GetSelectAsync(RoleQuery.SelectQuery query)
    {
        query.Result = (await _repository.GetListAsync((r) => r.Enable)).Select(role => new RoleItemResponse
        {
            Id = role.Id,
            Name = role.Name,
            Describe = role.Describe,
        }).ToList();
    }

    [EventHandler]
    public async Task GetRoleListByIdQuery(IdListQuery query)
    {
        query.Result = (await _repository.GetListAsync((r) => query.IdList.Contains(r.Id))).Select(role => new RoleItemResponse
        {
            Id = role.Id,
            Name = role.Name,
            Describe = role.Describe,
            Number = role.Number,
            Enable = role.Enable,
        }).ToList();
    }
}
