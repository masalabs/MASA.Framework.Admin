namespace Masa.Framework.Admin.Service.Authentication.Application.Roles;

public class RoleQueryHandler
{
    private readonly IRoleRepository _repository;
    private readonly DbContext _dbContext;
    private readonly IEventBus _eventBus;

    public RoleQueryHandler(IRoleRepository repository, AuthenticationDbContext dbContext, IEventBus eventBus)
    {
        _repository = repository;
        _dbContext = dbContext;
        _eventBus = eventBus;
    }

    [EventHandler]
    public async Task GetListAsync(RoleListQuery query)
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
            new PaginatedOptions
            {
                Page = query.PageIndex,
                PageSize = query.PageSize,
                Sorting = new Dictionary<string, bool>
                {
                    [nameof(Role.ModificationTime)] = true,
                    [nameof(Role.CreationTime)] = true,
                }
            });

        query.Result = new PaginatedItemResponse<RoleItemResponse>(query.PageIndex, query.PageSize, roles.Total, roles.TotalPages,
            roles.Result.Select(role
                => new RoleItemResponse
                {
                    Id = role.Id,
                    Name = role.Name,
                    Describe = role.Describe,
                    Number = role.Number,
                    Enable = role.Enable,
                    CreationTime = role.CreationTime
                }));
    }

    [EventHandler]
    public async Task GetRoleBaseAsync(RoleBaseQuery query)
    {
        var role = await _repository.FindAsync(query.RoleId);
        ArgumentNullException.ThrowIfNull(role, nameof(role));
        query.Result = new RoleInfo()
        {
            Name = role.Name,
            Describe = role.Describe,
            Number = role.Number,
            Enable = role.Enable,
            ChildrenRoleIds = role.RoleItems.Select(roleItem => roleItem.RoleId).ToList()
        };
    }

    /// <summary>
    /// Get the current role's own permission permissions (excluding inherited permissions)
    /// </summary>
    /// <param name="query"></param>
    [EventHandler]
    public Task GetPermissionAsync(RolePermissionQuery query)
    {
        query.Result =
            (from rolePermission in _dbContext.Set<RolePermission>().Include(rolePermission => rolePermission.Role)
             where rolePermission.Role.Id == query.RoleId
             join permission in _dbContext.Set<Permission>()
                 on rolePermission.PermissionsId equals permission.Id
                 into temp
             from newPermissions in temp.DefaultIfEmpty()
             select new AuthorizeItemResponse
             {
                 Id = rolePermission.Id,
                 PermissionId = newPermissions.Id,
                 PermissionName = newPermissions.Name,
                 ObjectType = newPermissions.ObjectType,
                 Resource = newPermissions.Resource,
                 Scope = newPermissions.Scope,
                 InheritanceRoleSource = rolePermission.Role.Name,
                 PermissionType = newPermissions.PermissionType,
             }).ToList();
        return Task.CompletedTask;
    }

    [EventHandler(Order = 1)]
    public async Task GetDetailAsync(RoleDetailQuery query)
    {
        var role = await _repository.FindAsync(query.RoleId);
        ArgumentNullException.ThrowIfNull(role, nameof(role));

        query.Result = new RoleDetailResponse
        {
            Id = role.Id,
            Name = role.Name,
            Describe = role.Describe,
            Number = role.Number,
            Enable = role.Enable,
            CreationTime = role.CreationTime,
            ChildrenRoleIds = role.RoleItems.Select(roleItem => roleItem.RoleId).ToList(),
            Permissions = await GetPermissionAsync(role.Id)
        };
    }

    [EventHandler]
    public async Task GetPermissionsByRolesAsync(PermissionsByRolesQuery query)
    {
        query.Result = await GetPermissionAsync(query.RoleIds);
    }

    private async Task<List<AuthorizeItemResponse>> GetPermissionAsync(Guid roleId)
    {
        var allChildrenRoleIdList = await GetRoleListLoop(roleId);

        var permissions = await (from rolePermission in _dbContext.Set<RolePermission>()
                    .Include(rolePermission => rolePermission.Role)
                    .Where(rolePermission => allChildrenRoleIdList.Contains(rolePermission.Role.Id))
                                 join permission in _dbContext.Set<Permission>() on rolePermission.PermissionsId equals permission.Id
                                     into temp
                                 from newPermissions in temp.DefaultIfEmpty()
                                 select new
                                 {
                                     rolePermission.Id,
                                     PermissionId = newPermissions.Id,
                                     PermissionName = newPermissions.Name,
                                     newPermissions.ObjectType,
                                     newPermissions.Resource,
                                     newPermissions.Scope,
                                     InheritanceRoleId = rolePermission.Role.Id,
                                     InheritanceRoleSource = rolePermission.Role.Name,
                                     newPermissions.PermissionType,
                                     newPermissions.Enable,
                                     newPermissions.Action
                                 })
            .ToListAsync();
        return permissions.Where(permission
                => (permission.InheritanceRoleId != roleId && permission.PermissionType == PermissionType.Public ||
                    permission.InheritanceRoleId == roleId) &&
                permission.Enable)
            .Select(permission => new AuthorizeItemResponse()
            {
                Id = permission.Id,
                PermissionId = permission.PermissionId,
                PermissionName = permission.PermissionName,
                ObjectType = permission.ObjectType,
                Resource = permission.Resource,
                Scope = permission.Scope,
                Action = permission.Action,
                InheritanceRoleSource = permission.InheritanceRoleSource,
                PermissionType = permission.PermissionType
            }).ToList();
    }

    private async Task<List<AuthorizeItemResponse>> GetPermissionAsync(List<Guid> roleIds)
    {
        var allChildrenRoleIdList = new List<Guid>();
        foreach(var roleId in roleIds)
        {
            allChildrenRoleIdList.AddRange(await GetRoleListLoop(roleId));
        }

        var permissions = await (from rolePermission in _dbContext.Set<RolePermission>()
                   .Include(rolePermission => rolePermission.Role)
                   .Where(rolePermission => allChildrenRoleIdList.Contains(rolePermission.Role.Id))
                                 join permission in _dbContext.Set<Permission>() on rolePermission.PermissionsId equals permission.Id
                                     into temp
                                 from newPermissions in temp.DefaultIfEmpty()
                                 select new
                                 {
                                     rolePermission.Id,
                                     PermissionId = newPermissions.Id,
                                     PermissionName = newPermissions.Name,
                                     newPermissions.ObjectType,
                                     newPermissions.Resource,
                                     newPermissions.Scope,
                                     InheritanceRoleId = rolePermission.Role.Id,
                                     InheritanceRoleSource = rolePermission.Role.Name,
                                     newPermissions.PermissionType,
                                     newPermissions.Enable,
                                     newPermissions.Action
                                 })
           .ToListAsync();
        return permissions.Where(permission
                => (roleIds.Contains(permission.InheritanceRoleId) is false && permission.PermissionType == PermissionType.Public ||
                    roleIds.Contains(permission.InheritanceRoleId)) &&
                permission.Enable)
            .Select(permission => new AuthorizeItemResponse()
            {
                Id = permission.Id,
                PermissionId = permission.PermissionId,
                PermissionName = permission.PermissionName,
                ObjectType = permission.ObjectType,
                Resource = permission.Resource,
                Scope = permission.Scope,
                Action = permission.Action,
                InheritanceRoleSource = permission.InheritanceRoleSource,
                PermissionType = permission.PermissionType
            }).ToList();
    }

    private async Task<List<Guid>> GetRoleListLoop(Guid roleId)
    {
        var query = new RoleCacheDetailQuery(roleId);
        await _eventBus.PublishAsync(query);

        var roleIdList = new List<Guid>();

        foreach (var childrenRoleId in query.Result.ChildrenRoleIds)
        {
            var childrenRoleIdList = await GetRoleListLoop(childrenRoleId);
            roleIdList.AddRange(childrenRoleIdList);
        }
        roleIdList.Add(roleId);
        return roleIdList;
    }


    // private async Task<RoleDetailDto> GetPermissionListByLoopAsync(Guid roleId)
    // {
    //     var role = await GetDetailAsync(roleId);
    //     if (role.ChildrenRoles.Any())
    //     {
    //         List<List<AuthorizeItemDto>> childrenRolePermissions = new();
    //         foreach (var childrenRoleId in role.ChildrenRoles)
    //         {
    //             var childrenRole = await GetPermissionListByLoopAsync(childrenRoleId);
    //             childrenRolePermissions.Add(childrenRole.Permissions);
    //         }
    //         var inheritPermissions =
    //             await GetPermissionListAsync(role.Permissions, await GetInheritPermissionListAsync(childrenRolePermissions));
    //         role.Permissions = inheritPermissions;
    //     }
    //     return role;
    // }
    //
    // /// <summary>
    // /// Get the last permission based on the current permission and the inherited permission set
    // /// </summary>
    // /// <param name="currentRolePermissions"></param>
    // /// <param name="inheritedPermissions"></param>
    // /// <returns></returns>
    // private Task<List<AuthorizeItemDto>> GetPermissionListAsync(
    //     List<AuthorizeItemDto> currentRolePermissions,
    //     List<AuthorizeItemDto> inheritedPermissions)
    // {
    //     foreach (var inheritedPermission in inheritedPermissions)
    //     {
    //         if (currentRolePermissions.All(permission => permission.PermissionId != inheritedPermission.PermissionId))
    //         {
    //             currentRolePermissions.Add(inheritedPermission);
    //         }
    //     }
    //     return Task.FromResult(inheritedPermissions);
    // }
    //
    // /// <summary>
    // /// get inherited permissions
    // /// </summary>
    // /// <param name="childrenPermissions"></param>
    // /// <returns></returns>
    // /// <exception cref="UserFriendlyException"></exception>
    // private Task<List<AuthorizeItemDto>> GetInheritPermissionListAsync(List<List<AuthorizeItemDto>> childrenPermissions)
    // {
    //     List<AuthorizeItemDto> permissions = new();
    //     foreach (var authorizeItems in childrenPermissions)
    //     {
    //         foreach (var authorizeItem in authorizeItems)
    //         {
    //             if (authorizeItem.PermissionType == PermissionType.Public)
    //             {
    //                 var item = permissions.FirstOrDefault(permission => permission.PermissionId == authorizeItem.PermissionId);
    //                 if (item != null)
    //                 {
    //                     if (item.PermissionEffect == PermissionEffect.Deny || authorizeItem.PermissionEffect == PermissionEffect.Deny)
    //                     {
    //                         item.PermissionEffect = PermissionEffect.Deny;
    //                     }
    //                 }
    //                 else
    //                 {
    //                     permissions.Add(new AuthorizeItemDto
    //                     {
    //                         Id = authorizeItem.Id,
    //                         RoleId = authorizeItem.RoleId,
    //                         RoleName = authorizeItem.RoleName,
    //                         PermissionId = authorizeItem.PermissionId,
    //                         PermissionEffect = authorizeItem.PermissionEffect,
    //                         PermissionType = authorizeItem.PermissionType
    //                     });
    //                 }
    //             }
    //             else if (authorizeItem.PermissionType == PermissionType.Private)
    //             {
    //                 //Private permissions are not inherited
    //             }
    //             else
    //                 throw new UserFriendlyException("Unsupported license type");
    //         }
    //     }
    //     return Task.FromResult(permissions);
    // }
    //
    // private async Task<RoleDetailDto> GetDetailAsync(Guid roleId)
    // {
    //     var role = await _repository.FindAsync(roleId);
    //     if (role == null)
    //         throw new UserFriendlyException("the role does not exist");
    //
    //     var list = new RoleDetailDto
    //     {
    //         Id = role.Id,
    //         Name = role.Name,
    //         Describe = role.Describe,
    //         Number = role.Number,
    //         Enable = role.Enable,
    //         CreationTime = role.CreationTime,
    //         ChildrenRoles = role.RoleItems.Select(roleItem => roleItem.ParentRoleId).ToList(),
    //         Permissions = role.Permissions.Select(permission => new AuthorizeItemDto
    //         {
    //             Id = permission.Id,
    //             RoleId = permission.Role.Id,
    //             RoleName = permission.Role.Name,
    //             PermissionId = permission.PermissionsId,
    //             PermissionType = permission.PermissionType,
    //         }).ToList()
    //     };
    //     return list;
    // }
    //
    // private class RoleDetailDto : RoleItemResponse
    // {
    //     public List<Guid> ChildrenRoles { get; set; } = new();
    //
    //     public List<AuthorizeItemDto> Permissions { get; set; } = new();
    // }
    //
    // private class AuthorizeItemDto
    // {
    //     /// <summary>
    //     /// Primary key id of Role and Permission relationship table
    //     /// </summary>
    //     public Guid Id { get; set; }
    //
    //     public Guid RoleId { get; set; } = default!;
    //
    //     /// <summary>
    //     /// The name of the role to which the license was granted
    //     /// </summary>
    //     public string RoleName { get; set; } = default!;
    //
    //     public Guid PermissionId { get; set; }
    //
    //     public PermissionType PermissionType { get; set; }
    // }

    [EventHandler]
    public async Task GetSelectAsync(SelectQuery query)
    {
        query.Result = (await _repository.GetListAsync((r) => r.Enable)).Select(role => new RoleItemResponse
        {
            Id = role.Id,
            Name = role.Name,
            Describe = role.Describe,
        }).ToList();
    }

    [EventHandler]
    public async Task GetAllRoleItemAsync(AllRoleItemQuery query)
    {
        query.Result = await _dbContext.Set<RoleItem>().Select(r => new RoleItemsResponse()
        {
            RoleId = r.RoleId,
            ParentRoleId = r.Role.Id
        }).ToListAsync();
    }

    [EventHandler]
    public async Task GetRoleListByIdQuery(IdListQuery query)
    {
        query.Result = (await _repository.GetListAsync((r) => query.IdList.Contains(r.Id)))
            .Select(role => new RoleItemResponse
        {
            Id = role.Id,
            Name = role.Name,
            Describe = role.Describe,
            Number = role.Number,
            Enable = role.Enable,
        }).ToList();
    }
}
